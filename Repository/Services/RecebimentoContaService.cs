using System;
using System.Data.SqlClient;
using System.Linq;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.DTO;
using IdxSistemas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IdxSistemas.AppRepository.Services
{
    public class RecebimentoContaService
    {

        private readonly DataContext db;

        private readonly string connString = "";

        private readonly IConfiguration configuration;

        public RecebimentoContaService(DataContext db,IConfiguration configuration){ 
             this.db = db;
             this.configuration = configuration;

             this.connString = configuration.GetConnectionString("sage_db");
        }


        public ComprovanteDTO GetComprovanteRecebimento(string Numero, DateTime Data, string Loja)
        {
            try
            {
                var recebimento = db.RecebimentoConta
                    .Include(r => r.ContaReceber)
                    .ThenInclude( c => c.Clientes )
                    .Where(e => e.NumeroDocumento == Numero && e.DataPagamento == Data &&
                                e.Loja == Loja && e.RowDeleted != "T")
                    .SingleOrDefault();


                var comprovante = new ComprovanteDTO
                {
                    Bairro = recebimento.ContaReceber.Clientes.Bairro,
                    Cidade = recebimento.ContaReceber.Clientes.Cidade,
                    CodigoCliente = recebimento.ContaReceber.Cliente,
                    CpfCliente = recebimento.ContaReceber.Clientes.Cpf,
                    DataEmissao = (DateTime)recebimento.ContaReceber.DataEmissao,
                    DataPagamento = (DateTime)recebimento.DataPagamento,
                    DataVencimento = (DateTime)recebimento.ContaReceber.DataVencimento,
                    Duplicata = recebimento.NumeroDuplicata,
                    Endereco = recebimento.ContaReceber.Clientes.Endereco,
                    Estado = recebimento.ContaReceber.Clientes.Estado,
                    Loja = recebimento.Loja,
                    NomeCliente = recebimento.ContaReceber.Clientes.Nome,
                    ValorPago = (double)recebimento.ValorPago,
                    ValorReceber = (double)recebimento.ContaReceber.ValorDuplicata
                };

                return comprovante;

            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}