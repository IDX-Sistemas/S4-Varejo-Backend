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
    public class ProdutosController : BaseController<Produto>
    {
        private readonly IConfiguration configuration;

        public ProdutosController(DataContext db, IConfiguration configuration) 
        {
            this.db = db;
            this.configuration = configuration;
        }

        [HttpGet]
        [ODataRoute("GetProdutoPeloCodigo(Codigo={codigo})")]
        public IActionResult GetProdutoPeloCodigo([FromODataUri] string codigo)
        {
            var service = new ProdutoService(this.db, this.configuration);
            try
            {
                return Ok(service.getProdutoPeloCodigo(codigo));
            }
            catch (Exception)
            {
                return Ok(null);
            }
        }

        [HttpGet]
        [ODataRoute("ExisteCodigoProduto(Codigo={codigo})")]
        public IActionResult ExisteCodigoProduto([FromODataUri] string codigo)
        {
            var service = new ProdutoService(this.db, this.configuration);
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
        [ODataRoute("GetDescricaoProduto(Codigo={codigo})")]
        public IActionResult GetDescricaoProduto([FromODataUri] string codigo)
        {
            var service = new ProdutoService(this.db, this.configuration);
            try
            {
                return Ok(service.getDescricao(codigo));
            }
            catch (Exception)
            {
                return Ok();
            }
        }


        [HttpGet]
        [ODataRoute("GetPrecoVistaProduto(Codigo={codigo})")]
        public IActionResult GetPrecoVistaProduto([FromODataUri] string codigo)
        {
            var service = new ProdutoService(this.db, this.configuration);
            try
            {
                return Ok(service.getPrecoVista(codigo));
            }
            catch (Exception)
            {
                return Ok(0d);
            }
        }

    }
}