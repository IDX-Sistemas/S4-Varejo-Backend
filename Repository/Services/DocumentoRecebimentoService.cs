using System;
using System.Data;
using System.Data.SqlClient;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.AppRepository.Utils;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace IdxSistemas.AppRepository.Services
{
    public class DocumentoRecebimentoService
    {
         private readonly DataContext db;

        private readonly string connString = "";

        private readonly IConfiguration configuration;

        public DocumentoRecebimentoService(DataContext db,IConfiguration configuration)
        { 
             this.db = db;
             this.configuration = configuration;
             this.connString = configuration.GetConnectionString("sage_db");
        }

        public string getNumeroDocumento(DateTime data, string loja)
        {
            var numero = "000001";

            try
            {
            
                using (var conn = new MySqlConnection( this.connString ))
                {
                    var sql = "SELECT MAX(NUM_DOC) AS NUM_DOC FROM pag_doc WHERE sql_deleted <> 'T' AND DAT_PAG = @DAT_PAG AND LOC_PAG = @LOC_PAG";
                    
                    if(conn.State == ConnectionState.Closed)
                        conn.Open();

                    var cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.Add( new MySqlParameter("@DAT_PAG", data.ToString("yyyy-MM-dd")));
                    cmd.Parameters.Add( new MySqlParameter("@LOC_PAG", loja));

                    var rs = cmd.ExecuteReader();

                    if (rs.HasRows)
                    {
                        if (rs.Read())
                        {
                            var ultimoNumero = SQLUtils.GetValue<int>(rs,"NUM_DOC");
                            numero = (ultimoNumero + 1).ToString().PadLeft( 6, '0');
                            this.setNumeroDocumento(ultimoNumero + 1, data, loja);
                        }
                    }
        
                }       

                return numero;

            } 
            catch (SqlException ex)  
            {
                throw ex;
            }

           
        }

        private void setNumeroDocumento(int numero, DateTime data, string loja)
        {
            try
            {
                using (var conn = new MySqlConnection( this.connString ))
                {
                    var sql = "INSERT INTO pag_doc(NUM_DOC, DAT_PAG, LOC_PAG) VALUES(@NUM_DOC, @DAT_PAG, @LOC_PAG)";
                    if(conn.State == ConnectionState.Closed)
                        conn.Open();

                    var cmd = new MySqlCommand(sql, conn); 
                    cmd.Parameters.Add( new MySqlParameter("@NUM_DOC", numero));
                    cmd.Parameters.Add( new MySqlParameter("@DAT_PAG", data));
                    cmd.Parameters.Add( new MySqlParameter("@LOC_PAG", loja)); 

                    cmd.ExecuteNonQuery();  
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

    }
}