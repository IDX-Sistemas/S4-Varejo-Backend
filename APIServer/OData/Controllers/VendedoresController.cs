using System;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.AppRepository.Services;
using IdxSistemas.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class VendedoresController : BaseController<Vendedor>
    {
        private readonly IConfiguration configuration;

        public VendedoresController(DataContext db, IConfiguration configuration) 
        {
            this.db = db;
            this.configuration = configuration;
        }


        [HttpGet]
        [ODataRoute("ExisteCodigoVendedor(Codigo={codigo})")]
        public IActionResult ExisteCodigoVendedor([FromODataUri] string codigo)
        {
            var service = new VendedorService(this.db, this.configuration);
            try
            {
                return Ok(service.existeCodigo(codigo));
            }
            catch (Exception)
            {
                return Ok(false);
            }
        }
        
    }
}