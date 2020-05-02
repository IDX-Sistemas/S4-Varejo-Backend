using System;
using System.Linq;
using IdxSistemas.AppRepository.Context;
using Microsoft.Extensions.Configuration;

namespace IdxSistemas.AppRepository.Services
{
    public class PreVendaService
    {
        private readonly DataContext db;

        private readonly IConfiguration configuration;

        public PreVendaService(DataContext db,  IConfiguration configuration)
        {
            this.db = db;
            this.configuration = configuration;
        }

        public int AtualizaStatus(string Numero, string Status)
        {
            try
            {
                var p = db.PreVenda.Where(e => e.Numero == Numero && e.RowDeleted != "T").SingleOrDefault();
                

                p.Finalizado = Status;
                return db.SaveChanges();   
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }

}