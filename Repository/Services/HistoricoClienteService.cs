using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.AppRepository.Utils;
using IdxSistemas.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace IdxSistemas.AppRepository.Services
{
    public class HistoricoClienteService
    {
        private readonly DataContext db;

        private readonly string connString = "";

        private readonly IConfiguration configuration;

        public HistoricoClienteService(DataContext db, IConfiguration configuration)
        {
            this.db = db;
            this.configuration = configuration;

            this.connString = configuration.GetConnectionString("sage_db");
        }


        public int ProcessaHistoricoCliente(string Codigo)
        {
            try
            {

                ExcluiHistorico(Codigo);

                // MONTA HISTORICO ATUALIZADO.
                db.ContaReceber
                     .Where(e => e.Cliente == Codigo && e.RowDeleted != "T")
                     .OrderBy( e => e.DataVencimento)
                     .ToList()
                     .ForEach(t => {

                         var historico = new HistoricoCliente
                         {
                             Cliente = t.Cliente,
                             NumeroDuplicata = t.NumeroDuplicata,
                             DataVencimento = (DateTime)t.DataVencimento,
                             DataEmissao = t.DataEmissao,
                             DataPagamento = GetDataPagamento(t.NumeroDuplicata, t.Loja),
                             Loja = t.Loja,
                             NumeroDocumento = t.NumeroDocumento,
                             TipoVenda = t.TipoVenda,
                             ValorDuplicata = t.ValorDuplicata,
                             ValorPago = GetValorPago(t.NumeroDuplicata, t.Loja)
                         };

                         db.HistoricoCliente.Add(historico);
                     });

                return db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        private void ExcluiHistorico(string Codigo)
        {
            try
            {
                using (var conn = new MySqlConnection(connString))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    var sql = @"DELETE FROM TMP_HISTORICO_CLIENTE WHERE Cliente = @Cliente";

                    var cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.Add(new MySqlParameter("@Cliente", Codigo));

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private double GetValorPago(string NumeroDuplicata, string Loja)
        {
            try
            {
                double valorPago = 0;

                using (var conn = new MySqlConnection(connString))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    var sql = @"SELECT SUM(VAL_PAG) VAL_PAG
                                  FROM fil_rec (NOLOCK)
                                  WHERE NUM_DUP = @NUM_DUP AND COD_LOC = @COD_LOC AND  
                                        sql_deleted <> 'T'";

                    var cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.Add(new MySqlParameter("@NUM_DUP", NumeroDuplicata));
                    cmd.Parameters.Add(new MySqlParameter("@COD_LOC", Loja));

                    var rs = cmd.ExecuteReader();

                    if ( rs.HasRows ) { 
                        while ( rs.Read() )
                        {
                            valorPago = SQLUtils.GetValue<double>(rs, "VAL_PAG");
                        }
                    }

                    return valorPago;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private DateTime? GetDataPagamento(string NumeroDuplicata, string Loja)
        {
            try
            {
                DateTime? dataPagamento = null;

                using (var conn = new MySqlConnection(connString))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    var sql = @"SELECT MAX(DAT_PAG) DAT_PAG
                                  FROM fil_rec (NOLOCK)
                                  WHERE NUM_DUP = @NUM_DUP AND COD_LOC = @COD_LOC AND  
                                        sql_deleted <> 'T'";

                    var cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.Add(new MySqlParameter("@NUM_DUP", NumeroDuplicata));
                    cmd.Parameters.Add(new MySqlParameter("@COD_LOC", Loja));

                    var rs = cmd.ExecuteReader();

                    if (rs.HasRows)
                    {
                        while (rs.Read())
                        {
                            dataPagamento = SQLUtils.GetValue<DateTime?>(rs, "DAT_PAG");
                        }
                    }

                    return dataPagamento;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}