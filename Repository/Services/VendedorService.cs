using System;
using System.Data.SqlClient;
using IdxSistemas.AppRepository.Context;
using Microsoft.Extensions.Configuration;

namespace IdxSistemas.AppRepository.Services
{
    public class VendedorService
    {

        private readonly DataContext db;

        private readonly string connString = "";

        private readonly IConfiguration configuration;

        public VendedorService(DataContext db,IConfiguration configuration){ 
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