using System;
using System.Data.SqlClient;
using System.Linq;
using IdxSistemas.AppRepository.Context;
using Microsoft.Extensions.Configuration;
using IdxSistemas.Models;

namespace IdxSistemas.AppRepository.Services
{
    public class SaldoEstoqueService
    {

        private readonly DataContext db;

        private readonly string connString = "";

        private readonly IConfiguration configuration;

        private readonly ParametrosService parametrosService;

        public SaldoEstoqueService(DataContext db,IConfiguration configuration){ 
            this.db = db;
            this.configuration = configuration;
            this.parametrosService = new ParametrosService(db, configuration);
            this.connString = configuration.GetConnectionString("sage_db");
        }

        public void SaidaItem(string Codigo, string Loja, int Quantidade)
        {
            var ESTOQUE_NEGATIVO = parametrosService.getParametro("ESTOQUE_NEGATIVO");
            
            try
            {
                var saldo = db.SaldoEstoque.Where(e => e.Codigo == Codigo && e.Loja == Loja && e.RowDeleted != "T").SingleOrDefault();
                if (saldo == null)
                {
                    saldo = new SaldoEstoque
                    {
                        Codigo = Codigo,
                        Loja = Loja,
                        SaldoAtual = (0 - Quantidade)
                    };

                    db.SaldoEstoque.Add(saldo);
                }
                else
                {
                    saldo.SaldoAtual -= Quantidade;
                }

                if (ESTOQUE_NEGATIVO == "N" && saldo.SaldoAtual < 0)
                    throw new Exception(Codigo + " Estoque negativo nao permitido");

                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void EntradaItem(string Codigo, string Loja, int Quantidade)
        {
            try
            {
                var saldo = db.SaldoEstoque.Where(e => e.Codigo == Codigo && e.Loja == Loja && e.RowDeleted != "T").SingleOrDefault();
                if (saldo == null)
                {
                    saldo = new SaldoEstoque
                    {
                        Codigo = Codigo,
                        Loja = Loja,
                        SaldoAtual = Quantidade
                    };

                    db.SaldoEstoque.Add(saldo);
                }
                else
                {
                    saldo.SaldoAtual += Quantidade;
                }

                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}