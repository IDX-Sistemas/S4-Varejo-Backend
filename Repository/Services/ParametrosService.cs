using System;
using System.Data;
using System.Data.SqlClient;
using IdxSistemas.AppRepository.Context;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

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
                using (var conn = new MySqlConnection(connString))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    var sql = "SELECT VAL_PAR FROM par_met WHERE VARIAVEL = @VARIAVEL AND sql_deleted <> 'T' ";
                    var cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.Add( new MySqlParameter("@VARIAVEL", Parametro));

                    return (string)cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}