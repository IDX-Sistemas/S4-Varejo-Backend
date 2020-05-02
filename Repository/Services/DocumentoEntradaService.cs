using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using IdxSistemas.AppRepository.Context;
using Microsoft.Extensions.Configuration;

using IdxSistemas.Models;
using IdxSistemas.AppRepository.Utils;
using MySql.Data.MySqlClient;
using System.Data;

namespace IdxSistemas.AppRepository.Services
{
    public class DocumentoEntradaService
    {

        private readonly DataContext db;

        private readonly string connString = "";

        private readonly IConfiguration configuration;

        private readonly SaldoEstoqueService saldoEstoqueService;
        private readonly ContaReceberService contaReceberService;


        public DocumentoEntradaService(DataContext db,IConfiguration configuration){ 
            this.db = db;
            this.configuration = configuration;
            this.connString = configuration.GetConnectionString("sage_db");
            this.saldoEstoqueService = new SaldoEstoqueService(db, configuration);
            this.contaReceberService = new ContaReceberService(db, configuration);
        }


        public int ClassificarDocumento(string Numero, string Fornecedor)
        {

            try
            {
                using (var tr = db.Database.BeginTransaction())
                {
                    var nf = db.DocumentoEntrada
                       .Where(e => e.Numero == Numero && e.Fornecedor == Fornecedor && e.RowDeleted != "T").SingleOrDefault();

                    if (nf == null)
                    {
                        throw new Exception("Documento de Entrada nao encontrado.");
                    }

                    if (nf.Classificacao == "S")
                    {
                        return 1;
                    }

                    CriaContasPagarDocumentoEntrada(Numero, Fornecedor);
                    ExcluiContaPagarTemp(Numero, Fornecedor);
                    EntradaItemsEstoque(Numero, Fornecedor);

                    nf.Classificacao = "S";

                    db.SaveChanges();
                    tr.Commit();

                    return 1;
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int EstornaClassificacaoDocumentoEntrada(string Numero, string Fornecedor)
        {
            try
            {
                using (var tr = db.Database.BeginTransaction())
                {
                    var nf = db.DocumentoEntrada
                       .Where(e => e.Numero == Numero && e.Fornecedor == Fornecedor && e.RowDeleted != "T").SingleOrDefault();

                    if (nf == null)
                    {
                        throw new Exception("Documento de Entrada nao encontrado.");
                    }


                    if (ExistePagamento(Numero, Fornecedor))
                    {
                        return 4;
                    }

                    ExcluiTitulos(Numero, Fornecedor);
                    CriaContaPagarTemp(Numero, Fornecedor);  // NAO MANTEM AJUSTES DE VENCIMENTO VALOR
                    SaidaItemsEstoque(Numero, Fornecedor);

                    nf.Classificacao = "N";
                    db.DocumentoEntrada.Update(nf);
                    db.SaveChanges();

                    tr.Commit();

                    return 1;
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool ExisteDocumentoEntrada(string Numero, string Fornecedor)
        {
            return db.DocumentoEntrada
                .Where(e => e.Numero == Numero && e.Fornecedor == Fornecedor && e.RowDeleted != "T").Count() > 0;
        }

        public bool ExistePagamento(string Numero, string Fornecedor)
        {
            return db.ContaPagar
                .Where(e => e.NotaFiscal == Numero && e.Fornecedor == Fornecedor && e.DataPagamento != null && e.RowDeleted != "T").Count() > 0;
        }

        private void ExcluiTitulos(string Numero, string Fornecedor)
        {
            try
            {
                db.ContaPagar.Where(e => e.NotaFiscal == Numero && e.Fornecedor == Fornecedor && e.RowDeleted != "T")
                    .ToList()
                    .ForEach( c => {
                        db.ContaPagar.Remove(c);
                    });

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CriaContasPagarDocumentoEntrada(string Numero, string Fornecedor)
        {
            try
            {

                db.ContaPagarTemp
                    .Where(e => e.NotaFiscal == Numero && e.Fornecedor == Fornecedor && e.RowDeleted != "T")
                    .ToList()
                    .ForEach( temp => {

                        ContaPagar cp = new ContaPagar
                        {
                            Fornecedor = temp.Fornecedor,
                            NotaFiscal = temp.NotaFiscal,
                            NumeroDocumento = temp.NumeroDocumento,
                            Classificacao = temp.Classificacao,
                            CodigoConta = temp.CodigoConta,
                            DataEmissao = temp.DataEmissao,
                            DataPagamento = temp.DataPagamento,
                            DataRecebimento = temp.DataRecebimento,
                            DataVencimento = temp.DataVencimento,
                            Desconto = temp.Desconto,
                            Duplicata = temp.Duplicata,
                            Historico = temp.Historico,
                            Juros = temp.Juros,
                            Loja = temp.Loja,
                            NumeroCheque = temp.NumeroCheque,
                            ValorDuplicata = temp.ValorDuplicata,
                            ValorPago = temp.ValorPago
                        };

                        db.ContaPagar.Add(cp);
                    
                    });
                
                db.SaveChanges();

                ExcluiContaPagarTemp(Numero, Fornecedor);

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SaidaItemsEstoque(string Numero, string Fornecedor)
        {
            try
            {
                db.DocumentoEntradaItem
                 .Where(e => e.DocumentoEntradaNumero == Numero && e.Fornecedor == Fornecedor &&  e.EntradaAntecipada != "S" && e.RowDeleted != "T")
                 .ToList()
                    .ForEach(item => {
                        saldoEstoqueService.SaidaItem(item.Codigo, item.Loja, (int)item.Quantidade);
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


        private void EntradaItemsEstoque(string Numero, string Fornecedor)
        {
            try
            {
                db.DocumentoEntradaItem
                    .Where(e => e.DocumentoEntradaNumero == Numero && e.Fornecedor == Fornecedor && e.EntradaAntecipada !="S" && e.RowDeleted != "T")
                    .ToList()
                    .ForEach(item => {
                        saldoEstoqueService.EntradaItem(item.Codigo, item.Loja, (int)item.Quantidade);
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Cria conta a pagar temporario para edicao antes da classificacao da
        /// Nota Fiscal
        /// </summary>
        /// <param name="Numero">Numero da Nota Fiscal</param>
        /// <param name="Fornecedor">Codigo do Fornecedor</param>
        public void CriaContaPagarTemp(string Numero, string Fornecedor)
        {
            try
            {
                var nf = db.DocumentoEntrada
                        .Where(e => e.Numero == Numero && e.Fornecedor == Fornecedor && e.RowDeleted != "T").SingleOrDefault();

                if (nf == null)
                {
                    throw new Exception("Documento de Entrada nao encontrado.");
                }

                ExcluiContaPagarTemp(Numero, Fornecedor);

                var condicao = db.CondicaoPagamento
                                .Where(e => e.Codigo == nf.Condicao).SingleOrDefault();

                if (condicao == null)
                {
                    throw new Exception("Condicao de pagamento nao informada.");
                }


                var parcelas = condicao.Parcelas;
                var intervalo = condicao.Intervalo;
                var totalNotaFiscal = nf.ValorTotal;

                try
                {
                    var vencimento = ((DateTime)nf.DataEmissao).AddDays(intervalo);
                    var valorDuplicata = totalNotaFiscal / parcelas;

                    for (int parcela = 1; parcela <= parcelas; parcela++)
                    {
                        var cp = new ContaPagarTemp
                        {
                            Classificacao = "",
                            CodigoConta = "",
                            DataEmissao = nf.DataEmissao,
                            DataPagamento = null,
                            DataRecebimento = nf.DataRecebimento,
                            DataVencimento = vencimento,
                            Desconto = 0,
                            Duplicata = nf.NumeroDuplicata + "/" + parcela.ToString(),
                            Fornecedor = nf.Fornecedor,
                            Historico = "NF " + nf.Numero,
                            Juros = 0,
                            Loja = nf.Loja,
                            NotaFiscal = nf.Numero,
                            NumeroCheque = "",
                            NumeroDocumento = "",
                            ValorDuplicata = valorDuplicata,
                            ValorPago = 0
                        };

                        db.ContaPagarTemp.Add(cp);

                        vencimento = ((DateTime)vencimento).AddDays(intervalo);
                    }

                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// Exclui contas a pagar temporario
        /// /// </summary>
        /// <param name="Numero">Numero da Nota Fiscal</param>
        /// <param name="Fornecedor">Codigo do Fornecedor</param>
        private void ExcluiContaPagarTemp(string Numero, String Fornecedor)
        {
            try
            {
                using (var conn = new MySqlConnection(connString))
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    var sql = "DELETE FROM tmp_con_pag WHERE NUM_NOT = @NUM_NOT AND COD_FOR = @COD_FOR";
                    var cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.Add(new MySqlParameter("@NUM_NOT", Numero));
                    cmd.Parameters.Add(new MySqlParameter("@COD_FOR", Fornecedor));
                    cmd.ExecuteNonQuery();
                }

            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }


        public List<object> getItems(string Numero, string Fornecedor)
        {
            List<object> items = new List<object>();;
            
            try
            {
                using (var conn = new MySqlConnection(connString))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    var sql = $@"SELECT A.COD_ITE, A.VAL_UNI, A.QUT_ITE, B.DES_ITE
                                  FROM ite_com A 
                                LEFT JOIN cad_ite B 
                                   ON B.COD_ITE = A.COD_ITE AND B.sql_deleted <> 'T' 
                                WHERE A.NUM_NOT = @NUM_NOT AND A.COD_FOR = @COD_FOR AND A.sql_deleted <> 'T' ";
                    
                    
                    var cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.Add( new MySqlParameter("@NUM_NOT", Numero));
                    cmd.Parameters.Add( new MySqlParameter("@COD_FOR", Fornecedor));

                    var rs = cmd.ExecuteReader();
                    if (rs.HasRows)
                    {
                        while (rs.Read())        
                        {
                            var unitario = SQLUtils.GetValue<double>(rs, "VAL_UNI");
                            var quantidade =  SQLUtils.GetValue<int>(rs, "QUT_ITE");
                           
                            items.Add( new {
                                Codigo = SQLUtils.GetValue<string>(rs, "COD_ITE"),
                                ValorUnitario = unitario ,
                                Quantidade = quantidade,
                                Descricao = SQLUtils.GetValue<string>(rs, "DES_ITE") ,
                                Total =  unitario * quantidade
                            });
                        }
                    }

                    return items;
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int clearItems(string Numero, string Fornecedor)
        {
           
            try
            {
                using (var conn = new MySqlConnection(connString))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    var sql = $@"DELETE ite_com WHERE NUM_NOT = @NUM_NOT AND COD_FOR = @COD_FOR AND sql_deleted <> 'T' ";
                    
                    var cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.Add( new MySqlParameter("@NUM_NOT", Numero));
                    cmd.Parameters.Add( new MySqlParameter("@COD_FOR", Fornecedor));

                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



       
    }
}