using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

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
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        
        public string getProximoCodigo()
        {
            
            var proximoCodigo = "00001";

            try{
            
                using (var conn = new MySqlConnection( this.connString ))
                {
                    var sql = "SELECT COD_CLI FROM cli_cod";
                    
                    if(conn.State == System.Data.ConnectionState.Closed)
                        conn.Open();

                    var cmd = new MySqlCommand(sql, conn);    
                    var codigo = cmd.ExecuteScalar();
                    proximoCodigo = (Convert.ToInt32(codigo) + 1).ToString().PadLeft(5,'0');
                }       
            
            } catch (Exception ex) {
            
                throw ex;
            
            }
            
            return proximoCodigo;
        }

        public void setProximoCodigo(string codigo)
        {
             try{
            
                using (var conn = new MySqlConnection( this.connString ))
                {
                    var sql = "UPDATE cli_cod SET COD_CLI = @COD_CLI";
                    
                    if(conn.State == ConnectionState.Closed)
                        conn.Open();

                    var cmd = new MySqlCommand(sql, conn); 
                    cmd.Parameters.Add(new MySqlParameter("@COD_CLI",codigo));

                    cmd.ExecuteNonQuery();
                }       
            
            } catch (Exception ex) {
            
                throw ex;
            
            }
        }

    }
}