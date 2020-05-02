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
    public class PedidoVendaController : BaseController<PedidoVenda>
    {
        private readonly IConfiguration configuration;
        private readonly PedidoVendaService pedidoVendaService;

        public PedidoVendaController(DataContext db, IConfiguration configuration)
        {
            this.db = db;
            this.configuration = configuration;
            this.pedidoVendaService = new PedidoVendaService(db, configuration);
        }


        public new IActionResult Post([FromBody] PedidoVenda t)
        {
            if (t.FlagEntrada == "S" && t.ValorEntrada <= 0 )
            {
                ModelState.AddModelError("ValorEntrada", "Valor da entrada nao informado.");
            }

            if (t.FlagEntrada == "S" && t.Parcelas <= 1)
            {
                ModelState.AddModelError("Parcelas", "Minimo de 2 parcelas");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                using (var tr = db.Database.BeginTransaction())
                {
                    db.PedidoVenda.Add(t);
                    db.SaveChanges();

                    pedidoVendaService.CriaContaReceberTemp(t.Numero, t.Loja);

                    tr.Commit();
                }
            }
            catch (DbUpdateException ex)
            { 
                var items = db.PedidoVendaItem
                   .Where(e => e.NumeroVenda == t.Numero && e.Loja == t.Loja && t.RowDeleted != "T");

                foreach (var item in items)
                {
                    db.PedidoVendaItem.Remove(item);
                }

                db.SaveChanges();

                if (EntityExists(t.RowId))
                {
                    return Conflict();
                }
                else
                {
                    ModelState.AddModelError(ex.Message, ex.StackTrace);
                    return BadRequest(ModelState);
                }
            }

            return Created(t);
        }

        [AcceptVerbs("PATCH", "MERGE")]
        public new IActionResult Patch([FromODataUri] long key, [FromBody] Delta<PedidoVenda> patch)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PedidoVenda t = db.PedidoVenda.Where(e => e.RowId == key && e.RowDeleted != "T").SingleOrDefault();

            if (t == null)
            {
                return NotFound();
            }

            patch.Patch(t);

            try
            {
                using (var tr = db.Database.BeginTransaction())
                {
                    db.SaveChanges();

                    pedidoVendaService.CriaContaReceberTemp(t.Numero, t.Loja);

                    tr.Commit();
                }
                
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!EntityExists(key))
                {
                    return NotFound();
                }
                else
                {
                    ModelState.AddModelError(ex.Message, ex.StackTrace);
                    return BadRequest(ModelState);
                }
            }

            return Updated(t);
        }

        public new IActionResult Delete([FromODataUri] long key)
        {
            PedidoVenda t = db.PedidoVenda.Where( e => e.RowId == key).SingleOrDefault();

            if (t == null)
            {
                return NotFound();
            }

            try
            {
                var items = db.PedidoVendaItem
                    .Where( e => e.NumeroVenda == t.Numero && e.Loja == t.Loja && t.RowDeleted != "T");
                
                foreach (var item in items)
                {
                    db.PedidoVendaItem.Remove(item);
                }

                db.PedidoVenda.Remove(t);
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ModelState.AddModelError(ex.Message, ex.StackTrace);
                return BadRequest(ModelState);
            }
           
            return NoContent();
        }


        private bool EntityExists(long key)
        {
            return db.PedidoVenda.Count(e => e.RowId == key) > 0;
        }

        [HttpGet]
        [ODataRoute("ExistePedidoVenda(Numero={numero}, Loja={loja})")]
        public IActionResult ExistePedidoVenda(string numero, string loja)
        {
            var service = new PedidoVendaService(this.db, this.configuration);
            try
            {
                return Ok( service.existePedido(numero, loja) );
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Message, ex.StackTrace);
                return BadRequest(ModelState);
            }
        }


        [HttpGet]
        [ODataRoute("PedidoVendaRecebido(Numero={numero}, Loja={loja})")]
        public IActionResult PedidoVendaRecebido(string numero, string loja)
        {
            var service = new PedidoVendaService(this.db, this.configuration);
            try
            {
                return Ok(service.existeRecebimento(numero, loja));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Message, ex.StackTrace);
                return BadRequest(ModelState);
            }
        }


        [HttpGet]
        [ODataRoute("FaturaPedido(Numero={numero}, Loja={loja})")]
        public IActionResult FaturaPedido(string numero, string loja)
        {
            var service = new PedidoVendaService(this.db, this.configuration);
            try
            {
                return Ok(service.FaturaPedido(numero, loja));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return BadRequest(ModelState);
            }
        }


        [HttpGet]
        [ODataRoute("EstornaFaturamentoPedido(Numero={numero}, Loja={loja})")]
        public IActionResult EstornaFaturamentoPedido(string numero, string loja)
        {
            var service = new PedidoVendaService(this.db, this.configuration);
            try
            {
                return Ok(service.EstornaFaturamentoPedido(numero, loja));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Message, ex.StackTrace);
                return BadRequest(ModelState);
            }
        }


        [HttpGet]
        [ODataRoute("CancelaPedido(Numero={numero}, Loja={loja})")]
        public IActionResult CancelaPedido(string numero, string loja)
        {
            var service = new PedidoVendaService(this.db, this.configuration);
            try
            {
                return Ok(service.CancelaPedido(numero, loja));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Message, ex.StackTrace);
                return BadRequest(ModelState);
            }
        }


        [HttpGet]
        [ODataRoute("GetDadosCarne(Numero={numero}, Loja={loja})")]
        public IActionResult GetDadosCarne(string numero, string loja)
        {
            var service = new PedidoVendaService(this.db, this.configuration);
            try
            {
                return Ok(service.GetDadosCarne(numero, loja));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Message, ex.StackTrace);
                return BadRequest(ModelState);
            }
        }
    }
}