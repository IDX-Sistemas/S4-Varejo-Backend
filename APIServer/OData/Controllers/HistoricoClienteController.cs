using System;
using System.Collections.Generic;
using System.Linq;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.AppRepository.Services;
using IdxSistemas.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class HistoricoClienteController : ODataController
    {
        private readonly DataContext db;
        private readonly IConfiguration configuration;

        public HistoricoClienteController(DataContext db, IConfiguration configuration)
        {
            this.db = db;
            this.configuration = configuration; 
        }

        [HttpGet]
        [ODataRoute("GetHistoricoCliente(Codigo={codigo})")]
        public IActionResult GetHistoricoCliente(string codigo)
        {
            try
            {
                var service = new HistoricoClienteService(db, configuration);
                service.ProcessaHistoricoCliente(codigo);

                return Ok(
                    db.HistoricoCliente
                        .Where(e => e.Cliente == codigo && e.RowDeleted != "T")
                        .OrderBy( e => e.DataVencimento )
                        .ThenBy( e => e.NumeroDuplicata )
                     );
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}