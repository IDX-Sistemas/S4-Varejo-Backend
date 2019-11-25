using System;
using System.Data.SqlClient;
using System.Linq;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.Models;
using Microsoft.Extensions.Configuration;

namespace IdxSistemas.AppRepository.Services
{
    public class ProdutoService
    {

        private readonly DataContext db;

        private readonly string connString = "";

        private readonly IConfiguration configuration;

        public ProdutoService(DataContext db,IConfiguration configuration){ 
             this.db = db;
             this.configuration = configuration;

             this.connString = configuration.GetConnectionString("sage_db");
        }


        public Produto getProdutoPeloCodigo(string codigo){

            try
            {
                Produto produto = db.Produtos.Where( e => e.Codigo == codigo && e.RowDeleted != "T").SingleOrDefault();
                return produto;
            }
            catch (System.Exception)
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

                    var sql = "SELECT sql_rowid FROM cad_ite WHERE COD_ITE = @COD_ITE AND sql_deleted <> 'T' ";
                    var cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add( new SqlParameter("@COD_ITE", Codigo));

                    var rs = cmd.ExecuteReader();

                    return rs.HasRows ;   
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public string getDescricao(string Codigo)
        {
            try
            {
                using (var conn = new SqlConnection(connString))
                {
                    if (conn.State == System.Data.ConnectionState.Closed)
                        conn.Open();

                    var sql = "SELECT DES_ITE FROM cad_ite WHERE COD_ITE = @COD_ITE AND sql_deleted <> 'T' ";
                    var cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add( new SqlParameter("@COD_ITE", Codigo));

                    return cmd.ExecuteScalar().ToString();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }


        public string getPrecoVista(string Codigo)
        {
            try
            {
                using (var conn = new SqlConnection(connString))
                {
                    if (conn.State == System.Data.ConnectionState.Closed)
                        conn.Open();

                    var sql = "SELECT VAL_001 FROM cad_ite WHERE COD_ITE = @COD_ITE AND sql_deleted <> 'T' ";
                    var cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add( new SqlParameter("@COD_ITE", Codigo));

                    return cmd.ExecuteScalar().ToString();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }


        public bool ExisteMarca(string Codigo)
        {
            try
            {
                using (var conn = new SqlConnection(connString))
                {
                    if (conn.State == System.Data.ConnectionState.Closed)
                        conn.Open();

                    var sql = "SELECT sql_rowid FROM cad_mar WHERE COD_MAR = @COD_MAR AND sql_deleted <> 'T' ";
                    var cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@COD_MAR", Codigo));

                    var rs = cmd.ExecuteReader();

                    return rs.HasRows;
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public bool ExisteTipo(string Codigo)
        {
            try
            {
                using (var conn = new SqlConnection(connString))
                {
                    if (conn.State == System.Data.ConnectionState.Closed)
                        conn.Open();

                    var sql = "SELECT sql_rowid FROM cad_ord WHERE COD_ORD = @COD_ORD AND sql_deleted <> 'T' ";
                    var cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@COD_ORD", Codigo));

                    var rs = cmd.ExecuteReader();

                    return rs.HasRows;
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

    }
}