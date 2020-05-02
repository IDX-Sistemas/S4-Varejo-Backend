using System;
using System.Linq;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.AppRepository.Services;
using IdxSistemas.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class EntradaAntecipadaController : BaseController<EntradaAntecipada>
    {
        private readonly IConfiguration configuration;
        private readonly EntradaAntecipadaService entradaAntecipadaService;
        private readonly ProdutoService produtoService;
        
        public EntradaAntecipadaController(DataContext db, IConfiguration configuration) 
        {
            this.db = db;
            this.configuration = configuration;
            this.entradaAntecipadaService = new EntradaAntecipadaService(db, configuration);
            this.produtoService = new ProdutoService(db, configuration);
        }

        
        [HttpPost]
        public new IActionResult Post([FromBody] EntradaAntecipada t)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var p = t.Produto.Split(' ');

            var codigoMarca = p[0].ToString();
            var codigoTipo = p[1].ToString();
            var codigoReferencia = p[2].ToString();

            if ( !produtoService.ExisteMarca(codigoMarca) )
            {
                ModelState.AddModelError("Marca", "Codigo da marca nao cadastrado.");
            }

            if ( !produtoService.ExisteTipo(codigoTipo) )
            {
                ModelState.AddModelError("Tipo", "Codigo do tipo nao cadastrado.");
            }

            if ( codigoReferencia.Length <= 0 )
            {
                ModelState.AddModelError("Referencia", "Informe o codigo do produto.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EntradaAntecipada.Add(t);

            try
            {
                db.SaveChanges();
                entradaAntecipadaService.EntradaAntecipada(t);
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return BadRequest(ModelState);
            }

            return Created(t);
        }


        [HttpDelete]
        public new IActionResult Delete([FromODataUri] long key)
        {
            EntradaAntecipada t = db.EntradaAntecipada.Where(e => e.RowId == key && e.RowDeleted != "T").SingleOrDefault();

            if (t == null)
            {
                return NotFound();
            }

            try
            {
                entradaAntecipadaService.EstornoEntradaAntecipada(t);

                db.EntradaAntecipada.Remove(t);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.GetType().ToString(), ex.Message);
                return BadRequest(ModelState);
            }


            return NoContent();
        }

    }
}