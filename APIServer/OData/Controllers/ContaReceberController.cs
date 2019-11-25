using System;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.AppRepository.Services;
using IdxSistemas.Models;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class ContaReceberController : BaseController<ContaReceber>
    {
        private readonly IConfiguration configuration;
        
        public ContaReceberController(DataContext db, IConfiguration configuration) 
        {
            this.db = db;
            this.configuration = configuration;
        }

        [HttpGet]
        [ODataRoute("GetContasReceberPorCliente(Codigo={codigo}, Database={database})")]
        public IActionResult GetContasReceberPorCliente(string codigo, string database)
        {
            var service = new ContaReceberService(this.db, this.configuration);
            
            try
            {
                DateTime data = DateTime.Parse(database);
                return Ok(service.getContasReceberPorCliente(codigo, data));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [ODataRoute("AjustaVencimentoContaReceber(Id={id}, Data={data}, Valor={valor})")]
        public IActionResult AjustaVencimentoContaReceber(long id, string data, double valor)
        {
            var service = new ContaReceberService(this.db, this.configuration);

            try
            {
                DateTime vencimento = DateTime.Parse(data);
                return Ok(service.ajustaVencimento(id, vencimento, valor));
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}