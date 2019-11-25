using IdxSistemas.AppRepository.Context;
using IdxSistemas.AppRepository.Services;
using IdxSistemas.Models;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdxSistemas.AppServer.OData.Controllers 
{
    public class PreVendaController : BaseController<PreVenda>
    {
        private readonly IConfiguration configuration;

        public PreVendaController(DataContext db, IConfiguration configuration)
        {
            this.db = db;
            this.configuration = configuration;
        }

        [HttpGet]
        [ODataRoute("AtualizaStatusPreVenda(Numero={numero}, Status={status})")]
        public IActionResult AtualizaStatusPreVenda(string numero, string status)
        {
            var service = new PreVendaService(this.db, this.configuration);
            try
            {
                return Ok(service.AtualizaStatus(numero, status));
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
