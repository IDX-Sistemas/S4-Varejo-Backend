using System;
using System.Data;
using System.Data.SqlClient;
using IdxSistemas.AppRepository.Context;
using Microsoft.Extensions.Configuration;

namespace IdxSistemas.AppRepository.Services
{
    public class ParametrosService
    {

        private readonly DataContext db;

        private readonly string connString = "";

        private readonly IConfiguration configuration;

        public ParametrosService(DataContext db,IConfiguration configuration){ 
             this.db = db;
             this.configuration = configuration;

             this.connString = configuration.GetConnectionString("sage_db");
        }

        public string getParametro(string Parametro)
        {
            try
            {
                using (var conn = new SqlConnection(connString))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    var sql = "SELECT VAL_PAR FROM par_met WHERE VARIAVEL = @VARIAVEL AND sql_deleted <> 'T' ";
                    var cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add( new SqlParameter("@VARIAVEL", Parametro));

                    return (string)cmd.ExecuteScalar();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        
    }
}