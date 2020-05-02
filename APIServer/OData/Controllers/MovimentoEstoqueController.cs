using System;
using System.Linq;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.AppRepository.Services;
using IdxSistemas.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class MovimentoEstoqueController : ODataController
    {
        private readonly DataContext db;
        private readonly SaldoEstoqueService service;
        private readonly ProdutoService produtoService;
        private readonly IConfiguration configuration;

        private const string SALDO_INICIAL = "01";
        private const string ACERTO_MAIOR  = "02";
        private const string ACERTO_MENOR  = "03";
        private const string TRANSFERENCIA = "04";

        public MovimentoEstoqueController(DataContext db, IConfiguration configuration)
        {
            this.db = db;
            this.configuration = configuration;
            this.service = new SaldoEstoqueService(db, configuration);
            this.produtoService = new ProdutoService(db, configuration);
        }

        [HttpGet, EnableQuery]
        public IQueryable<MovimentoEstoque> Get()
        {
            return db.MovimentoEstoque.Where(e => e.RowDeleted != "T");
        }

        [HttpGet, EnableQuery]
        public SingleResult<MovimentoEstoque> Get([FromODataUri] long key)
        {
            return SingleResult.Create(
                db.MovimentoEstoque.Where(x => x.RowId == key && x.RowDeleted != "T"));
        }


        [HttpPost]
        public IActionResult Post([FromBody] MovimentoEstoque t)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (t.CodigoTransferencia == TRANSFERENCIA && t.LocalEstoqueEntrada == t.LocalEstoqueSaida)
            {
                ModelState.AddModelError("LocalEstoque", "Local de Estoque nao pode ser igual na transferencia.");
                return BadRequest(ModelState);
            }

            if (!produtoService.existeCodigo(t.CodigoItem))
            {
                ModelState.AddModelError(string.Empty, "Produto nao cadastrato.");
                return BadRequest(ModelState);
            }

            if (t.QuantidadeAtual <= 0 )
            {
                ModelState.AddModelError(string.Empty, "Informe Quantidade maior que zero.");
                return BadRequest(ModelState);
            }

            try
            {
                switch (t.CodigoTransferencia)
                {
                    case SALDO_INICIAL:
                        service.EntradaItem(t.CodigoItem, t.LocalEstoqueEntrada, t.QuantidadeAtual);
                        break;
                    
                    case ACERTO_MAIOR:
                        service.EntradaItem(t.CodigoItem, t.LocalEstoqueEntrada, t.QuantidadeAtual);
                        break;
                    
                    case ACERTO_MENOR:
                        service.SaidaItem(t.CodigoItem, t.LocalEstoqueSaida, t.QuantidadeAtual);
                        break;
                    
                    case TRANSFERENCIA:
                        service.SaidaItem(t.CodigoItem, t.LocalEstoqueSaida, t.QuantidadeAtual);
                        service.EntradaItem(t.CodigoItem, t.LocalEstoqueEntrada, t.QuantidadeAtual);
                        break;
                    
                    default:
                        throw new Exception("Codigo do movimento de estoque invalido.");
                }

                db.MovimentoEstoque.Add(t);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                if (EntityExists(t.RowId))
                {
                    return Conflict();
                }
                else
                {
                    ModelState.AddModelError(ex.GetType().ToString(), ex.Message) ;
                    return BadRequest(ModelState);
                }

            }

            return Created(t);
        }

        private bool EntityExists(long key)
        {
            return db.MovimentoEstoque.Count(e => e.RowId == key && e.RowDeleted != "T") > 0;
        }


    }
}