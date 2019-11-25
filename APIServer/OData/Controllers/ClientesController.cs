using System;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.Models;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;

using IdxSistemas.AppRepository.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNet.OData;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class ClientesController : BaseController<Cliente>
    {
        
        private readonly IConfiguration configuration;

        public ClientesController(DataContext db, IConfiguration configuration) {
            this.db = db;
            this.configuration = configuration;
        }

        [HttpPost]
        public new IActionResult Post([FromBody] Cliente t)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Clientes.Add(t);
            var service = new ClienteService(this.db, this.configuration);

            try
            {
                db.SaveChanges();
                service.setProximoCodigo(t.Codigo);
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
        public new IActionResult Patch([FromODataUri] long key, [FromBody] Delta<Cliente> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Cliente t = db.Clientes.Where( e => e.RowId == key && e.RowDeleted != "T").SingleOrDefault();

            if (t == null)
            {
                return NotFound();
            }

            patch.Patch(t);
           
            try
            {
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



         [HttpDelete]
        public new IActionResult Delete([FromODataUri] long key)
        {
            Cliente t = db.Clientes.Where( e => e.RowId == key && e.RowDeleted != "T").SingleOrDefault();

            if (t == null)
            {
                return NotFound();
            }

            try
            {
                db.Clientes.Remove(t);
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                
                throw;
            }
           

            return NoContent();
        }

        private bool EntityExists(long key)
        {
            return db.Clientes.Count(e => e.RowId == key) > 0;
        }

        [HttpGet]
        [ODataRoute("GetProximoCodigoCliente()")]
        public IActionResult GetProximoCodigoCliente()
        {
            var service = new ClienteService(this.db, this.configuration);
            try
            {
                return Ok(service.getProximoCodigo());
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet]
        [ODataRoute("GetClientePeloCodigo(Codigo={codigo})")]
        public IActionResult GetClientePeloCodigo(string codigo)
        {
            var service = new ClienteService(this.db, this.configuration);
            try
            {
                return Ok(service.getClientePeloCodigo(codigo));
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}