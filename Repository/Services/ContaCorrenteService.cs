using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.AppRepository.Utils;
using IdxSistemas.DTO;
using IdxSistemas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using IdxSistemas.Models.Tipos.ContaReceber;

namespace IdxSistemas.AppRepository.Services
{
    public class ContaCorrenteService
    {

        private readonly DataContext db;

        private readonly string connString = "";

        private readonly IConfiguration configuration;

        public ContaCorrenteService(DataContext db,IConfiguration configuration){ 
             this.db = db;
             this.configuration = configuration;

             this.connString = configuration.GetConnectionString("sage_db");
        }

        public int BaixaDuplicataPagar(string Fornecedor, string Duplicata, DateTime DataPagamento, double? ValorPago, 
                                        string CodigoConta, string NumeroCheque)
        {
            try
            {
                var cp = db.ContaPagar
                            .Where(e => e.Fornecedor == Fornecedor && e.Duplicata == Duplicata && e.RowDeleted != "T")
                            .SingleOrDefault();

                if (cp == null)
                {
                    throw new Exception("Duplicata nao localizada.");
                }

                cp.DataPagamento = DataPagamento;
                cp.ValorPago = ValorPago;
                cp.CodigoConta = CodigoConta;
                cp.NumeroCheque = NumeroCheque;

                db.ContaPagar.Update(cp);

                return db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public int EstornaBaixaDuplicataPagar(string Fornecedor, string Duplicata)
        {
            try
            {
                var cp = db.ContaPagar
                            .Where(e => e.Fornecedor == Fornecedor && e.Duplicata == Duplicata && e.RowDeleted != "T")
                            .SingleOrDefault();

                if (cp == null)
                {
                    throw new Exception("Duplicata nao localizada.");
                }

                cp.DataPagamento = null;
                cp.ValorPago = 0.00;
                cp.CodigoConta = "";
                cp.NumeroCheque = "";

                db.ContaPagar.Update(cp);

                return db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}