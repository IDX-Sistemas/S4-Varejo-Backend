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
    public class SecaoController : BaseController<Secao>
    {
        private readonly IConfiguration configuration;
        
        public SecaoController(DataContext db, IConfiguration configuration) 
        {
            this.db = db;
            this.configuration = configuration;
        }


        [HttpGet]
        [ODataRoute("ExisteCodigoSecao(Codigo={codigo})")]
        public IActionResult ExisteCodigoSecao([FromODataUri] string codigo)
        {
            var service = new SecaoService(this.db, this.configuration);
            try
            {
                return Ok(service.existeCodigo(codigo));
            }
            catch (Exception)
            {
                return Ok(false);
            }
        }


        [HttpGet]
        [ODataRoute("CalculaVendaSecao(LojaDe={lojaDe}, LojaAte={lojaAte}, DataInicial={dataInicial}, DataFinal={dataFinal})")]
        public IActionResult CalculaVendaSecao(string lojaDe, string lojaAte, DateTime dataInicial, DateTime dataFinal)
        {
            var service = new VendaSecaoService(this.db, this.configuration);

            try
            {
                service.CalculaVendaSecao(lojaDe, lojaAte, dataInicial, dataFinal);
                return Ok(""); 
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("CalculaVendaSecao", ex.Message);
                return BadRequest(ModelState);
            }
        }
    }
}