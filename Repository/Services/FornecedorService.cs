using System;
using System.Data.SqlClient;
using IdxSistemas.AppRepository.Context;
using Microsoft.Extensions.Configuration;

namespace IdxSistemas.AppRepository.Services
{
    public class FornecedorService
    {

        private readonly DataContext db;

        private readonly string connString = "";

        private readonly IConfiguration configuration;

        public FornecedorService(DataContext db,IConfiguration configuration){ 
             this.db = db;
             this.configuration = configuration;

             this.connString = configuration.GetConnectionString("sage_db");
        }

        public string getProximoCodigo()
        {
            
            var proximoCodigo = "0001";

            try{
            
                using (SqlConnection conn = new SqlConnection( this.connString ))
                {
                    var sql = "SELECT MAX(COD_FOR) FROM cad_for WHERE COD_FOR <> '9999'";
                    
                    if(conn.State == System.Data.ConnectionState.Closed)
                        conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);    
                    var codigo = cmd.ExecuteScalar();
                    proximoCodigo = (Convert.ToInt32(codigo) + 1).ToString().PadLeft(4,'0');
                }       
            
            } catch (Exception) {
            
                throw;
            
            }
            
            return proximoCodigo;
        }
    }
}