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
    public class PedidoVendaItemController : BaseController<PedidoVendaItem>
    {
        private readonly IConfiguration configuration;
        public PedidoVendaItemController(DataContext db, IConfiguration configuration)
        {
            this.db = db;
            this.configuration = configuration;
        }

        public new IActionResult Post([FromBody] PedidoVendaItem t)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            var preVendaService = new PreVendaService(db, configuration);

            db.PedidoVendaItem.Add(t);

            try
            {
                Produto p = db.Produtos.Where(e => e.Codigo == t.Codigo && e.RowDeleted != "T").SingleOrDefault();
                if (p != null)
                {
                    p.Secao = t.Secao;
                    db.Produtos.Update(p);
                }

                db.SaveChanges();

                if (!string.IsNullOrEmpty(t.NumeroPreVenda))
                {
                    var finalizado = "S";
                    preVendaService.AtualizaStatus(t.NumeroPreVenda, finalizado);
                }

            }
            catch (DbUpdateException)
            {

                if (EntityExists(t.RowId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(t);
        }

        [AcceptVerbs("PATCH", "MERGE")]
        public new IActionResult Patch([FromODataUri] long key, [FromBody] Delta<PedidoVendaItem> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PedidoVendaItem t = db.PedidoVendaItem.Where(e => e.RowId == key && e.RowDeleted != "T").SingleOrDefault();

            if (t == null)
            {
                return NotFound();
            }

            patch.Patch(t);

            try
            {
                Produto p = db.Produtos.Where(e => e.Codigo == t.Codigo && e.RowDeleted != "T").SingleOrDefault();
                if (p != null)
                {
                    p.Secao = t.Secao;
                    db.Produtos.Update(p);
                }

                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(t);
        }

        private bool EntityExists(long key)
        {
            return db.PedidoVendaItem.Count(e => e.RowId == key) > 0;
        }
    }
    
}