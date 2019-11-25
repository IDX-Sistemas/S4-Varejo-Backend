using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.AppRepository.Utils;
using Microsoft.Extensions.Configuration;

using IdxSistemas.Models;

namespace IdxSistemas.AppRepository.Services
{
    public class VendaSecaoService
    {

        private readonly DataContext db;

        private readonly string connString = "";

        private readonly IConfiguration configuration;

        public VendaSecaoService(DataContext db,IConfiguration configuration){ 
             this.db = db;
             this.configuration = configuration;

             this.connString = configuration.GetConnectionString("sage_db");
        }


        public void CalculaVendaSecao(string lojaDe, string lojaAte, DateTime dataInicial, DateTime dataFinal)
        {

            LimpaTabelaVendaSecao();
            
            
            try
            {
                using (var conn = new SqlConnection(connString))
                {
                    if (conn.State == System.Data.ConnectionState.Closed)
                        conn.Open();


                    var result = 
                        db.Lojas
                        .Where(e => e.Codigo.CompareTo(lojaDe) >=0 && e.Codigo.CompareTo(lojaAte) <= 0 && e.RowDeleted != "T")
                        .Select( r => r.Codigo )
                        .ToList();

                    result.ForEach(loja =>
                    {
                        var sql = @"  SELECT b.NUM_VEN, a.COD_SEC, b.TIP_VEN, b.VAL_VEN,
                                             CAST( ((a.QUT_ITE * a.VAL_UNI) - a.VAL_DES ) AS FLOAT) AS TOTAL
	                                    FROM ite_not a
	                               LEFT JOIN cad_not b
		                                  ON b.NUM_VEN = a.NUM_VEN 
	                               LEFT JOIN cad_sec c
		                                  ON c.COD_SEC = a.COD_SEC
	                                   WHERE b.DAT_VEN BETWEEN @DATA_INICIAL AND @DATA_FINAL AND 
		                                     b.COD_LOC = @LOJA AND 
		                                     a.sql_deleted <> 'T' AND  b.sql_deleted <> 'T' AND c.sql_deleted <> 'T' AND a.COD_SEC IS NOT NULL 
                                    ORDER BY a.COD_SEC";

                        var cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.Add(new SqlParameter("@DATA_INICIAL", dataInicial));
                        cmd.Parameters.Add(new SqlParameter("@DATA_FINAL", dataFinal));
                        cmd.Parameters.Add(new SqlParameter("@LOJA", loja));
                        

                        var rs = cmd.ExecuteReader();

                        double? valorVista = 0;
                        double? valorPrazo = 0;
                        double? valorEntrada = 0;
                        
                        while (rs.Read())
                        {
                            var numeroVenda = SQLUtils.GetValue<string>(rs, "NUM_VEN");
                            var codigoSecao = SQLUtils.GetValue<string>(rs, "COD_SEC");
                            var tipoVenda = SQLUtils.GetValue<string>(rs, "TIP_VEN");
                            var temEntrada = SQLUtils.GetValue<string>(rs, "FLA_ENT");
                            var totalSecao = SQLUtils.GetValue<double>(rs, "TOTAL");
                            var totalVenda = SQLUtils.GetValue<double>(rs, "VAL_VEN");

                            if (tipoVenda == "1")
                            {
                                valorVista = totalSecao;
                            }
                            else
                            {
                                if (temEntrada == "S")
                                {
                                    var percentual = totalSecao / totalVenda;
                                    var valor = db.ContaReceber
                                        .Where(e => e.NumeroDuplicata == numeroVenda + "/0" && e.RowDeleted != "T")
                                        .Select( r => r.ValorDuplicata ).SingleOrDefault();

                                    valorEntrada = valor * percentual;

                                    valorPrazo = totalSecao - valorEntrada;
                                }
                                else
                                {
                                    valorPrazo = totalSecao;
                                }
                            }


                            var obj = db.VendaSecao
                                        .Where(e => e.Loja == loja && e.Secao == codigoSecao && e.RowDeleted != "T")
                                        .SingleOrDefault();


                            if (obj == null)
                            {
                                obj = new VendaSecao{
                                    Loja = loja,
                                    Secao = codigoSecao,
                                    ValorEntrada = valorEntrada,
                                    ValorPrazo = valorPrazo,
                                    ValorVista = valorVista
                                };

                                db.VendaSecao.Add(obj);
                            }
                            else
                            {
                                obj.ValorEntrada += valorEntrada;
                                obj.ValorPrazo += valorPrazo;
                                obj.ValorVista += valorVista;

                                db.VendaSecao.Update(obj);
                            }

                            db.SaveChanges();
                        }

                    });
                 
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        private void LimpaTabelaVendaSecao()
        {
            try
            {
                using (var conn = new SqlConnection(connString))
                {
                    if (conn.State == System.Data.ConnectionState.Closed)
                        conn.Open();

                    var sql = "DELETE FROM arq_vxx";

                    var cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool existeCodigo(string Codigo)
        {
            try
            {
                using (var conn = new SqlConnection(connString))
                {
                    if (conn.State == System.Data.ConnectionState.Closed)
                        conn.Open();

                    var sql = "SELECT sql_rowid FROM cad_ven WHERE COD_VEN = @COD_VEN AND sql_deleted <> 'T' ";
                    var cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add( new SqlParameter("@COD_VEN", Codigo));

                    var rs = cmd.ExecuteReader();

                    return rs.HasRows ;   
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public string getNomeVendedor(string Codigo)
        {
            try
            {
                using (var conn = new SqlConnection(connString))
                {
                    if (conn.State == System.Data.ConnectionState.Closed)
                        conn.Open();

                    var sql = "SELECT NOM_VEN FROM cad_ven WHERE COD_VEN = @COD_VEN AND sql_deleted <> 'T' ";
                    var cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add( new SqlParameter("@COD_VEN", Codigo));

                    return cmd.ExecuteScalar().ToString();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
 
    }
}