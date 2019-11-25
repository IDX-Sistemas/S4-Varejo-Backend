using System;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.Models;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;

using IdxSistemas.AppRepository.Services;
using Microsoft.Extensions.Configuration;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class FornecedoresController : BaseController<Fornecedor>
    {
        private readonly IConfiguration configuration;

        public FornecedoresController(DataContext db, IConfiguration configuration)
        {
            this.db = db;
            this.configuration = configuration;
        }

        [HttpGet]
        [ODataRoute("GetProximoCodigoFornecedor()")]
        public IActionResult GetProximoCodigoFornecedor()
        {
            var service = new FornecedorService(this.db, this.configuration);
            try
            {
                return Ok(service.getProximoCodigo());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}