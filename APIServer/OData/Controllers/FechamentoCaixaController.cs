using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using IdxSistemas.Models;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.AppRepository.Services;

using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class FechamentoCaixaController : BaseController<FechamentoCaixa>
    {

        private readonly FechamentoCaixaService service;

        public FechamentoCaixaController(DataContext db)
        {
            this.db = db;
            this.service = new FechamentoCaixaService(this.db);
        }

        [HttpGet]
        [ODataRoute("GeraFechamentoCaixa(Loja={loja}, Data={data})")]
        public IActionResult GeraFechamentoCaixa(string loja, DateTime data)
        {
            try
            {
                this.service.GeraFechamentoCaixa(loja, data);
                return Ok();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return BadRequest(ModelState);
            }

        }

        [HttpGet]
        [ODataRoute("FechamentoCaixaLancado(Loja={loja}, Data={data})")]
        public IActionResult FechamentoCaixaLancado(string loja, DateTime data)
        {
            try
            {
                return Ok( this.service.CaixaLancado(loja, data) );
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return BadRequest(ModelState);
            }

        }

    }
}
