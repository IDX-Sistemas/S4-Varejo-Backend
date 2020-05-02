using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.AppRepository.Utils;
using IdxSistemas.DTO;
using IdxSistemas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using IdxSistemas.Models.Tipos.ContaReceber;
using System.Data;
using MySql.Data.MySqlClient;

namespace IdxSistemas.AppRepository.Services
{
    public class ContaReceberService
    {

        private readonly DataContext db;

        private readonly string connString = "";

        private readonly IConfiguration configuration;

        public ContaReceberService(DataContext db,IConfiguration configuration){ 
             this.db = db;
             this.configuration = configuration;

             this.connString = configuration.GetConnectionString("sage_db");
        }


        public List<ContaReceberDTO> getContasReceberPorCliente(string codigo, DateTime database)
        {
            var parametrosService = new ParametrosService(this.db, this.configuration);
            var taxaJurosDiaria = decimal.Parse( parametrosService.getParametro("TAXA_JUROS_CREDIARIO") );
            
            List<ContaReceberDTO> titulos = new List<ContaReceberDTO>();

            try
            {
                using (var conn = new MySqlConnection(connString))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    var sql = @"SELECT NUM_DUP
                                  FROM con_rec WHERE COD_CLI = @COD_CLI AND FLA_PAG <> '1' AND  sql_deleted <> 'T' 
                              ORDER BY DAT_VEN, NUM_DUP";
                    
                    var cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.Add( new MySqlParameter("@COD_CLI", codigo));

                    var rs = cmd.ExecuteReader();

                    while (rs.Read())
                    {
                            var numeroDuplicata = SQLUtils.GetValue<string>(rs, "NUM_DUP");

                            var titulo = getTituloCalculado(numeroDuplicata, database);
                            if (titulo != null)
                                titulos.Add(titulo);
                        
                    }

                    return titulos;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        public bool ajustaVencimento(long id, DateTime vencimento, double valor)
        {
            try
            {
                var cr = db.ContaReceber.Where(e => e.RowId == id).SingleOrDefault();
                if (cr == null)
                {
                    return false;
                }

                cr.DataVencimento = vencimento;
                cr.ValorDuplicata = valor;

                db.ContaReceber.Update(cr);
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private ContaReceberDTO getTituloCalculado(string Numero, DateTime Database)
        {
            try
            {
                ContaReceberDTO receber = null;

                var titulo = db.ContaReceber.Where( e => e.NumeroDuplicata == Numero && e.FlagPgto != 1 && e.RowDeleted != "T").SingleOrDefault();
                if (titulo == null)
                {
                    return null;
                }
                
                var parametrosService = new ParametrosService(this.db, this.configuration);
                var taxaJurosDiaria = double.Parse( parametrosService.getParametro("TAXA_JUROS_CREDIARIO") );

                var venc = (DateTime) titulo.DataVencimento;

                var valorRecebido = this.getTotalRecebidoPorDuplicata(Numero);
                var dataPagamento = this.getDataPagamentoDuplicata(Numero);

                var valorReceber = Math.Round( ((double)titulo.ValorDuplicata + (titulo.Juros ?? 0) - (titulo.Desconto ?? 0)) - valorRecebido, 2 );

                if (dataPagamento != default(DateTime))
                {
                    if (dataPagamento.CompareTo(titulo.DataVencimento) > 0)
                        venc = dataPagamento;
                }

                double juros    = 0;
                double juros2   = 0;
                double desconto = 0;

                var hoje = Database;
                var atraso = hoje.Subtract(venc).Days;
                if (atraso <= 3)
                {
                    atraso = 0; 
                }
                else
                {

                    // CALCULO LEGADO DE ARREDONDAMENTO
                    var taxaJurosTotal = atraso * taxaJurosDiaria;
                    juros = Math.Truncate( (double)((valorReceber / 100) * taxaJurosTotal * 10 ) ) / 10;
                    juros2= Math.Truncate( (double)((valorReceber / 100) * taxaJurosTotal * 100) ) / 100;

                    if ( (juros2 - juros) >= 0.05)
                    {
                        juros += 0.10;
                    }
                }
                        
                var total = valorReceber + juros - desconto;
                        
                if (total > 0){
                    
                    receber = new ContaReceberDTO{
                        RowId = titulo.RowId,
                        NumeroDuplicata = titulo.NumeroDuplicata,
                        Loja = titulo.Loja,
                        NumeroDocumento = titulo.NumeroDocumento,
                        TipoVenda = titulo.TipoVenda,
                        DataEmissao = titulo.DataEmissao,
                        DataVencimento = (DateTime) titulo.DataVencimento,
                        ValorDuplicata = Math.Round((double)titulo.ValorDuplicata, 2),
                        Juros = Math.Round(juros, 2),
                        Desconto = Math.Round(desconto, 2),
                        Saldo = Math.Round(valorReceber, 2),
                        ValorReceber = Math.Round(total, 2),
                        Atraso = atraso
                    };
                }
    
                return receber;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int recebimentoTitulo(
            string NumeroDuplicata, DateTime DataPagamento, double ValorPago, string NumeroDocumento, string Loja,
            double ValorJuros, double ValorDesconto, int Quitar){

                try
                {
                    var tituloCalculado = getTituloCalculado(NumeroDuplicata, DataPagamento);

                    if (ValorPago < tituloCalculado.Saldo && Quitar == 1)
                    {
                        ValorDesconto = (double) tituloCalculado.Saldo - ValorPago;
                    }

                    var recebimento = new RecebimentoConta{
                        NumeroDuplicata = NumeroDuplicata,
                        DataPagamento = DataPagamento,
                        ValorPago = Math.Round(ValorPago, 2),
                        NumeroDocumento =  NumeroDocumento,
                        Loja = Loja,
                        ValorJuros = Math.Round(ValorJuros, 2),
                        ValorDesconto = Math.Round(ValorDesconto, 2)
                    };

                    db.RecebimentoConta.Add(recebimento);

                    var titulo = db.ContaReceber.Where( e => e.NumeroDuplicata == NumeroDuplicata && e.RowDeleted != "T" ).SingleOrDefault();
                    
                    if (titulo.Juros == null)
                        titulo.Juros = 0;
                    if (titulo.Desconto == null)
                        titulo.Desconto = 0;

                    titulo.Juros += ValorJuros;
                    titulo.Desconto += ValorDesconto;

                    if (Quitar == 1)
                    {
                        titulo.FlagPgto = StatusContaReceber.PAGO;
                    }

                    db.ContaReceber.Update(titulo);

                    return db.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    throw ex;
                }

        }


        public int estornaRecebimento(string Numero, string Loja, string Documento, DateTime Data)
        {   
            try
            {
                var titulo = db.ContaReceber
                    .Where( e => 
                        e.NumeroDuplicata == Numero && e.Loja == Loja && e.RowDeleted != "T" ).SingleOrDefault();
                
                if (titulo == null)
                {
                    return -1;    // titulo nao encontrato
                }

                if (Documento == null)
                    Documento = string.Empty;

                var recebimento = db.RecebimentoConta
                    .Where( e => 
                        e.NumeroDuplicata == Numero && 
                        e.Loja == Loja && 
                        e.NumeroDocumento == Documento &&
                        e.DataPagamento == Data &&
                        e.RowDeleted != "T" ).SingleOrDefault();
                
                if (recebimento == null)
                {
                    return -2;    // recebimento nao encontrado.
                }

                if (recebimento.ValorJuros != null && titulo.Juros != null)
                {
                    titulo.Juros -= recebimento.ValorJuros;           
                }

                if (recebimento.ValorPago != null && titulo.Desconto != null)
                {
                    titulo.Desconto -= recebimento.ValorDesconto;           
                }

                titulo.FlagPgto = 0;

                db.RecebimentoConta.Remove(recebimento);
                db.ContaReceber.Update(titulo);

                return db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DateTime getDataPagamentoDuplicata(string Numero)
        {
            try
            {
                DateTime? data = null;

                using (var conn = new MySqlConnection(connString))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    var sql = @"SELECT MAX(DAT_PAG) DATA
                                  FROM fil_rec WHERE NUM_DUP = @NUM_DUP AND sql_deleted <> 'T' ";
                    
                    var cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.Add( new MySqlParameter("@NUM_DUP", Numero));

                    var rs = cmd.ExecuteReader();

                    while (rs.Read())
                    {
                        data = SQLUtils.GetValue<DateTime>(rs, "DATA");
                    }

                    return (DateTime) data;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public double getTotalRecebidoPorDuplicata(string Numero)
        {
            try
            {
                double total = 0;

                using (var conn = new MySqlConnection(connString))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    var sql = @"SELECT SUM(VAL_PAG) AS TOTAL
                                  FROM fil_rec WHERE NUM_DUP = @NUM_DUP AND sql_deleted <> 'T' ";
                    var cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.Add( new MySqlParameter("@NUM_DUP", Numero));

                    var rs = cmd.ExecuteReader();

                    while (rs.Read())
                    {
                        total = SQLUtils.GetValue<double>(rs, "TOTAL");
                    }

                    return total;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}