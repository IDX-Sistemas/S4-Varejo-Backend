using System;
using System.Linq;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.Models;
using IdxSistemas.Models.Tipos.PedidoVenda;
using IdxSistemas.Models.Tipos.ContaReceber;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using IdxSistemas.DTO;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Data;
using MySql.Data.MySqlClient;

namespace IdxSistemas.AppRepository.Services
{
    public class PedidoVendaService
    {
        private readonly DataContext db;

        private readonly IConfiguration configuration;

        private readonly string connectionString;

        public PedidoVendaService(DataContext db,  IConfiguration configuration)
        {
            this.db = db;
            this.configuration = configuration;
            this.connectionString = configuration.GetConnectionString("sage_db");
        }

        public CarneDTO GetDadosCarne(string Numero, string Loja)
        {
            try
            {
                var pedido = db.PedidoVenda
                                .Include(c => c.Clientes)
                                .Where(e => e.Numero == Numero && e.Loja == Loja && e.RowDeleted != "T")
                                .SingleOrDefault();


                List<CarneVencimentoDTO> vencimentos = new List<CarneVencimentoDTO>();
                db.ContaReceber
                    .Where(e => e.NumeroCI == Numero && e.Loja == Loja && e.FlagPgto != 1 && e.RowDeleted != "T")
                    .OrderBy( v => v.DataVencimento )
                    .ToList()
                    .ForEach(cr => {
                        var vencimento = new CarneVencimentoDTO
                        {
                            Vencimento = (DateTime)cr.DataVencimento,
                            Valor = (double)cr.ValorDuplicata
                        };

                        vencimentos.Add(vencimento);
                    });


                var parcelas = (int)pedido.Parcelas;

                if (pedido.FlagEntrada == "S")
                    parcelas--;
                
                
                var carne = new CarneDTO
                {
                    Bairro = pedido.Clientes.Bairro,
                    Cidade = pedido.Clientes.Cidade,
                    CodigoCliente = pedido.Clientes.Codigo,
                    CpfCliente = pedido.Clientes.Cpf,
                    Emissao = (DateTime)pedido.Data,
                    Endereco = pedido.Clientes.Endereco,
                    Estado = pedido.Clientes.Estado,
                    NomeCliente = pedido.Clientes.Nome,
                    NumeroCi = Numero,
                    Parcelas = parcelas,
                    ValorTotal = (double)pedido.Total,
                    Vencimentos = vencimentos
                };

                return carne;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public bool existePedido(string Numero, string Loja)
        {
            return db.PedidoVenda
                    .Count(e => e.Numero == Numero && e.Loja == Loja && e.RowDeleted != "T") > 0;
        }


        public int FaturaPedido(string Numero, string Loja)
        {

            try
            {
                using (var tr = db.Database.BeginTransaction())
                {
                    var pedido = db.PedidoVenda.Where(e => e.Numero == Numero && e.Loja == Loja && e.RowDeleted != "T").SingleOrDefault();
                    if (pedido == null)
                    {
                        return Status.NAO_ENCONTRADO;
                    }

                    if (pedido.Faturado == Faturado.SIM)
                    {
                        return Status.PEDIDO_FATURADO;
                    }

                    CriaContaReceberPeloPedido(Numero, Loja);
                    ExcluiContaReceberTemp(Numero, Loja);
                    SaidaItemsEstoque(Numero, Loja);

                    pedido.Faturado = Faturado.SIM;

                    db.SaveChanges();
                    tr.Commit();

                    return Status.OK;
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public int EstornaFaturamentoPedido(string Numero, string Loja)
        {
            try
            {
                using (var tr = db.Database.BeginTransaction() )
                {
                    var pedido = db.PedidoVenda.Where(e => e.Numero == Numero && e.Loja == Loja && e.RowDeleted != "T").SingleOrDefault();
                    if (pedido == null)
                    {
                        return Status.NAO_ENCONTRADO;
                    }

                    if (existeRecebimento(Numero, Loja))
                    {
                        return Status.PEDIDO_RECEBIDO;
                    }

                    excluiTitulos(Numero, Loja);
                    CriaContaReceberTemp(Numero, Loja);  // NAO MANTEM AJUSTES DE VENCIMENTO VALOR
                    EntradaItemsEstoque(Numero, Loja);

                    pedido.Faturado = Faturado.NAO;
                    db.PedidoVenda.Update(pedido);
                    db.SaveChanges();
                    
                    tr.Commit();

                    return Status.OK;
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 


        private void SaidaItemsEstoque(string Numero, string Loja)
        {
            var saldoEstoqueService = new SaldoEstoqueService(db, configuration);

            try
            {
                db.PedidoVendaItem
                    .Where(e => e.NumeroVenda == Numero && e.Loja == Loja && e.RowDeleted != "T")
                    .ToList()
                    .ForEach(item => {
                        saldoEstoqueService.SaidaItem(item.Codigo, item.Loja, (int)item.Quantidade);
                    });

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void EntradaItemsEstoque(string Numero, string Loja)
        {
            var saldoEstoqueService = new SaldoEstoqueService(db, configuration);

            try
            {
                using (var tr = db.Database.BeginTransaction() )
                {
                    db.PedidoVendaItem
                        .Where(e => e.NumeroVenda == Numero && e.Loja == Loja && e.RowDeleted != "T")
                        .ToList()
                        .ForEach(item => {
                            saldoEstoqueService.EntradaItem(item.Codigo, item.Loja, (int)item.Quantidade);
                        });
                    tr.Commit();
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CancelaPedido(string Numero, String Loja) 
        {
            try
            {
                var pedido = db.PedidoVenda
                       .Where(e => e.Numero == Numero && e.Loja == Loja && e.RowDeleted != "T").SingleOrDefault();

                if (pedido == null)
                {
                    return Status.NAO_ENCONTRADO;
                }

                if (pedido.Faturado == Faturado.SIM)
                {
                    return Status.PEDIDO_FATURADO;
                }

                pedido.Faturado = Faturado.CANCELADO;
                db.PedidoVenda.Update(pedido);
                db.SaveChanges();

                return Status.OK;
            }
            catch (Exception ex)
            {

                throw ex;
            }   
        }
        public void CriaContaReceberTemp(string Numero, String Loja)
        {
            try
            {
                var pedido = db.PedidoVenda
                        .Where(e => e.Numero == Numero && e.Loja == Loja && e.RowDeleted != "T").SingleOrDefault();

                if (pedido == null)
                {
                    return;
                }

                ExcluiContaReceberTemp(Numero, Loja);

                if (pedido.TipoVenda == TipoVenda.CARNE || pedido.TipoVenda == TipoVenda.DUPLICATA || pedido.TipoVenda == TipoVenda.CHEQUE)
                {
                    var parcelas = pedido.Parcelas;
                    var valorTotal = pedido.Total;
                    var parcela = 1;

                    if (pedido.FlagEntrada == "S")
                    {
                        parcelas--;
                        valorTotal -= pedido.ValorEntrada;

                        var cr = new ContaReceberTemp
                        {
                            Cliente = pedido.Cliente,
                            DataEmissao = pedido.Data,
                            DataVencimento = (DateTime)pedido.Data,
                            Desconto = 0,
                            FlagEntrada = pedido.FlagEntrada,
                            FlagPgto = StatusContaReceber.PAGO,
                            Juros = 0,
                            Loja = pedido.Loja,
                            NumeroCI = pedido.Numero,
                            NumeroDocumento = pedido.NumeroDocumento,
                            NumeroDuplicata = pedido.Numero + "/0",
                            NumeroFatura = string.Empty,
                            TipoVenda = TipoVenda.VISTA,
                            ValorDuplicata = Math.Round((double)pedido.ValorEntrada, 2),
                            ValorFatura = Math.Round((double)pedido.Total, 2)
                        };

                        db.ContaReceberTemp.Add(cr);
                        db.SaveChanges();
                    }

                    if (parcelas > 0)
                    {
                        var valorParcela = valorTotal / parcelas;
                        var vencimento = ((DateTime)pedido.Data).AddDays(30);

                        for (; parcela <= parcelas; parcela++)
                        {
                            var cr = new ContaReceberTemp
                            {
                                Cliente = pedido.Cliente,
                                DataVencimento = vencimento,
                                Desconto = 0,
                                FlagEntrada = "N",
                                FlagPgto = StatusContaReceber.PENDENTE,
                                Juros = 0d,
                                Loja = pedido.Loja,
                                NumeroCI = pedido.Numero,
                                NumeroDocumento = pedido.NumeroDocumento,
                                NumeroDuplicata = pedido.Numero + "/" + parcela.ToString(),
                                NumeroFatura = string.Empty,
                                TipoVenda = pedido.TipoVenda,
                                ValorDuplicata = Math.Round((double)valorParcela, 2),
                                ValorFatura = Math.Round((double)pedido.Total, 2)
                            };

                            db.ContaReceberTemp.Add(cr);

                            vencimento = vencimento.AddDays(30);
                        }

                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        private void ExcluiContaReceberTemp(string Numero, string Loja)
        {
            try
            {
                using(var conn = new MySqlConnection(connectionString))
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    var sql = "DELETE FROM tmp_con_rec WHERE NUM__CI = @NUM__CI AND LOC_PAG = @LOC_PAG";
                    var cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.Add(new MySqlParameter("@NUM__CI", Numero));
                    cmd.Parameters.Add(new MySqlParameter("@LOC_PAG", Loja));
                    cmd.ExecuteNonQuery();
                }

            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }



        public int CriaContaReceberPeloPedido(string Numero, string Loja)
        {
            try
            {
                if (!existeRecebimento(Numero, Loja))
                {
                    excluiTitulos(Numero, Loja);

                    PedidoVenda pedido = db.PedidoVenda
                        .Where(e => e.Numero == Numero && e.Loja == Loja && e.RowDeleted != "T").SingleOrDefault();

                    switch (pedido.TipoVenda)
                    {
                        case TipoVenda.VISTA:
                            PagamentoVista(pedido);
                            break;

                        case TipoVenda.CARTAO:
                            PagamentoCartao(pedido);
                            break;

                        default:
                            PagamentoPrazo(pedido);
                            break;
                    }

                    return Status.OK;
                }
                else
                {
                    return Status.PEDIDO_RECEBIDO;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void PagamentoCartao(PedidoVenda pedido)
        {
            try
            {
                var parcelas = pedido.Parcelas;
                var valorTotal = pedido.Total;
                var parcela = 1;
                
                if (pedido.FlagEntrada == "S")
                {

                    parcelas--;
                    valorTotal -= pedido.ValorEntrada;

                    ContaReceber cr = new ContaReceber
                    {
                        Cliente = pedido.Cliente,
                        DataEmissao = pedido.Data,
                        DataVencimento = (DateTime)pedido.Data,
                        Desconto = 0,
                        FlagEntrada = pedido.FlagEntrada,
                        FlagPgto = StatusContaReceber.PAGO,
                        Juros = 0,
                        Loja = pedido.Loja,
                        NumeroCI = pedido.Numero,
                        NumeroDocumento = pedido.NumeroDocumento,
                        NumeroDuplicata = pedido.Numero + "/0",
                        NumeroFatura = string.Empty,
                        TipoVenda = TipoVenda.VISTA,
                        ValorDuplicata = Math.Round((double)pedido.ValorEntrada, 2),
                        ValorFatura = Math.Round((double)pedido.Total, 2)
                    };

                    db.ContaReceber.Add(cr);
                    db.SaveChanges();

                    var contaReceberService = new ContaReceberService(db, configuration);

                    contaReceberService.recebimentoTitulo(
                            cr.NumeroDuplicata, (DateTime)cr.DataEmissao, (double)cr.ValorDuplicata, string.Empty, cr.Loja,
                            (double)cr.Juros, (double)cr.Desconto, (short)cr.FlagPgto
                        );
                }

                if (parcelas > 0)
                {
                    var valorParcela = valorTotal / parcelas;

                    var plano = db.Operadoras.Where(e => e.RowId == pedido.OperadoraId && e.RowDeleted != "T").Single();
                    var taxaCartao = plano.Taxa;
                    var clienteId = plano.Admin;
                    var intervalo = plano.TipoVencimento;

                    var vencimento = ((DateTime)pedido.Data).AddDays(intervalo);
                    var valorDesconto = (valorParcela / 100) * taxaCartao;

                    for (; parcela <= parcelas; parcela++)
                    {
                        ContaReceber cr = new ContaReceber
                        {
                            Cliente = clienteId,
                            DataVencimento = vencimento,
                            Desconto = Math.Round((double)valorDesconto, 2),
                            FlagEntrada = "N",
                            FlagPgto = StatusContaReceber.PENDENTE,
                            Juros = 0d,
                            Loja = pedido.Loja,
                            NumeroCI = pedido.Numero,
                            NumeroDocumento = pedido.NumeroDocumento,
                            NumeroDuplicata = pedido.Numero + "/" + parcela.ToString(),
                            NumeroFatura = string.Empty,
                            TipoVenda = pedido.TipoVenda,
                            ValorDuplicata = Math.Round((double)valorParcela, 2),
                            ValorFatura = Math.Round((double)pedido.Total, 2)
                        };

                        db.ContaReceber.Add(cr);

                        vencimento = vencimento.AddDays(intervalo);
                    }

                    db.SaveChanges();
                }
                
            }
            catch (Exception ex )
            {
                throw ex;
            }
        }

        private void PagamentoPrazo(PedidoVenda pedido)
        {
            var contaReceberService = new ContaReceberService(db, configuration);
            
            try
            {

                db.ContaReceberTemp
                    .Where(e => e.NumeroCI == pedido.Numero && e.Loja == pedido.Loja && e.RowDeleted != "T")
                    .ToList()
                    .ForEach(temp => {
                        ContaReceber cr = new ContaReceber
                        {
                            Cliente = temp.Cliente,
                            DataEmissao = temp.DataEmissao,
                            DataVencimento = temp.DataVencimento,
                            Desconto = 0,
                            FlagEntrada = temp.FlagEntrada,
                            FlagPgto = temp.FlagPgto,
                            Juros = 0,
                            Loja = temp.Loja,
                            NumeroCI = temp.NumeroCI,
                            NumeroDocumento = temp.NumeroDocumento,
                            NumeroDuplicata = temp.NumeroDuplicata,
                            NumeroFatura = string.Empty,
                            TipoVenda = temp.TipoVenda,
                            ValorDuplicata = temp.ValorDuplicata,
                            ValorFatura = temp.ValorFatura
                        };

                        db.ContaReceber.Add(cr);
                        db.SaveChanges();

                        if (cr.FlagEntrada == "S")
                        {
                            contaReceberService.recebimentoTitulo(
                                cr.NumeroDuplicata, (DateTime)cr.DataEmissao, (double)cr.ValorDuplicata, string.Empty, cr.Loja,
                                (double)cr.Juros, (double)cr.Desconto, (short)cr.FlagPgto
                            );
                        }

                });
                    
                ExcluiContaReceberTemp(pedido.Numero, pedido.Loja);

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void PagamentoVista(PedidoVenda pedido)
        {
            try
            {
                ContaReceber cr = new ContaReceber
                {
                    Cliente = pedido.Cliente,
                    DataEmissao = pedido.Data,
                    DataVencimento = (DateTime)pedido.Data,
                    Desconto = pedido.ValorDesconto,
                    FlagEntrada = pedido.FlagEntrada,
                    FlagPgto = StatusContaReceber.PENDENTE,
                    Juros = Math.Round( (double) pedido.ValorAcrescimo, 2),
                    Loja = pedido.Loja,
                    NumeroCI = pedido.Numero,
                    NumeroDocumento = pedido.NumeroDocumento,
                    NumeroDuplicata = pedido.Numero + "/0",
                    NumeroFatura = string.Empty,
                    TipoVenda = pedido.TipoVenda,
                    ValorDuplicata = Math.Round( (double) pedido.Total, 2),
                    ValorFatura = Math.Round((double)pedido.Total, 2)
                };

                db.ContaReceber.Add(cr);
                db.SaveChanges();

                var contaReceberService = new ContaReceberService(db, configuration);

                contaReceberService.recebimentoTitulo(
                        cr.NumeroDuplicata, (DateTime)cr.DataEmissao, (double)cr.ValorDuplicata, string.Empty, cr.Loja,
                        (double)cr.Juros, (double)cr.Desconto, (short)cr.FlagPgto
                    );

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool existeRecebimento(string Numero, string Loja)
        {
            return db.RecebimentoConta
                .Count(e => e.NumeroDuplicata.StartsWith(Numero) && e.Loja == Loja && e.RowDeleted != "T") > 0;
        }

        private void excluiTitulos(string Numero, string Loja)
        {
            try
            {
                db.ContaReceber.Where(e => e.NumeroCI == Numero && e.Loja == Loja).ToList().ForEach(item =>
                {
                    db.ContaReceber.Remove(item);
                });

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

}