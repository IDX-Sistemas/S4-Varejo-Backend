using System;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.AppRepository.Services;
using IdxSistemas.Models;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class RecebimentoContaController : BaseController<RecebimentoConta>
    {
        private readonly IConfiguration configuration;
        private readonly RecebimentoContaService recebimentoContaService;
        private readonly ContaReceberService contaReceberService;

        public RecebimentoContaController(DataContext db, IConfiguration configuration) 
        {
            this.db = db;
            this.configuration = configuration;
            this.recebimentoContaService = new RecebimentoContaService(db, configuration);
            this.contaReceberService = new ContaReceberService(db, configuration);
        }

        [HttpGet]
        [ODataRoute(@"RecebimentoTitulo(NumeroDuplicata={NumeroDuplicata}, DataPagamento={DataPagamento}, ValorPago={ValorPago},
                                        NumeroDocumento={NumeroDocumento}, Loja={Loja}, ValorJuros={ValorJuros},
                                        ValorDesconto={ValorDesconto}, Quitar={Quitar})")]
        public IActionResult RecebimentoTitulo(
            string NumeroDuplicata, DateTime DataPagamento, double ValorPago, string NumeroDocumento, string Loja,
            double ValorJuros, double ValorDesconto, int Quitar
        )
        {
            try
            {
                return Ok(
                    contaReceberService.recebimentoTitulo(NumeroDuplicata, DataPagamento, ValorPago, NumeroDocumento,
                        Loja, ValorJuros, ValorDesconto, Quitar
                    )
                );
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [ODataRoute(@"EstornoRecebimento(NumeroDuplicata={NumeroDuplicata}, Loja={Loja}, NumeroDocumento = {NumeroDocumento}, DataPagamento = {DataPagamento})")]
        public IActionResult EstornoRecebimento(string NumeroDuplicata,string Loja, string NumeroDocumento, string DataPagamento)
        {
            try
            {
                return Ok(contaReceberService.estornaRecebimento(NumeroDuplicata, Loja, NumeroDocumento, DateTime.Parse(DataPagamento)) );
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet]
        [ODataRoute(@"GetComprovanteRecebimento(NumeroDocumento = {NumeroDocumento}, DataPagamento = {DataPagamento}, Loja={Loja})")]
        public IActionResult GetComprovanteRecebimento(string NumeroDocumento, string DataPagamento, string Loja)
        {
            try
            {
                return Ok(
                    recebimentoContaService
                        .GetComprovanteRecebimento(NumeroDocumento, DateTime.Parse(DataPagamento), Loja));
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}