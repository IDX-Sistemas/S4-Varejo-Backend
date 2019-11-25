using System;
using System.Data.SqlClient;
using IdxSistemas.AppRepository.Context;
using Microsoft.Extensions.Configuration;

namespace IdxSistemas.AppRepository.Services
{
    public class SecaoService
    {

        private readonly DataContext db;

        private readonly string connString = "";

        private readonly IConfiguration configuration;

        public SecaoService(DataContext db,IConfiguration configuration){ 
             this.db = db;
             this.configuration = configuration;

             this.connString = configuration.GetConnectionString("sage_db");
        }

        public bool existeCodigo(string Codigo)
        {
            try
            {
                using (var conn = new SqlConnection(connString))
                {
                    if (conn.State == System.Data.ConnectionState.Closed)
                        conn.Open();

                    var sql = "SELECT sql_rowid FROM cad_sec WHERE COD_SEC = @COD_SEC AND sql_deleted <> 'T' ";
                    var cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add( new SqlParameter("@COD_SEC", Codigo));

                    var rs = cmd.ExecuteReader();

                    return rs.HasRows ;   
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        
    }
}