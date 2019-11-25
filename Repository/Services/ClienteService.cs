using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.Models;
using Microsoft.Extensions.Configuration;

namespace IdxSistemas.AppRepository.Services
{
    public class ClienteService
    {

        private readonly DataContext db;

        private readonly string connString = "";

        private readonly IConfiguration configuration;

        public ClienteService(DataContext db,IConfiguration configuration){ 
             this.db = db;
             this.configuration = configuration;

             this.connString = configuration.GetConnectionString("sage_db");
        }

        public Cliente getClientePeloCodigo(string Codigo){
            try
            {
                return db.Clientes.Where( e => e.Codigo ==Codigo && e.RowDeleted != "T").SingleOrDefault();
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
        
        public string getProximoCodigo()
        {
            
            var proximoCodigo = "00001";

            try{
            
                using (SqlConnection conn = new SqlConnection( this.connString ))
                {
                    var sql = "SELECT COD_CLI FROM cli_cod";
                    
                    if(conn.State == System.Data.ConnectionState.Closed)
                        conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);    
                    var codigo = cmd.ExecuteScalar();
                    proximoCodigo = (Convert.ToInt32(codigo) + 1).ToString().PadLeft(5,'0');
                }       
            
            } catch (Exception) {
            
                throw;
            
            }
            
            return proximoCodigo;
        }

        public void setProximoCodigo(string codigo)
        {
             try{
            
                using (SqlConnection conn = new SqlConnection( this.connString ))
                {
                    var sql = "UPDATE cli_cod SET COD_CLI = @COD_CLI";
                    
                    if(conn.State == System.Data.ConnectionState.Closed)
                        conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn); 
                    cmd.Parameters.Add(new SqlParameter("@COD_CLI",codigo));

                    cmd.ExecuteNonQuery();
                }       
            
            } catch (Exception) {
            
                throw;
            
            }
        }

    }
}