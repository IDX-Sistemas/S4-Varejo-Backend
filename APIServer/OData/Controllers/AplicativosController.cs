using IdxSistemas.AppRepository.Context;
using IdxSistemas.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class AplicativosController : ODataController
    {
        private readonly DataContext db;

        public AplicativosController(DataContext db) => this.db = db;

        [HttpGet, EnableQuery]
        public IQueryable<Aplicativo> Get()
        {
            return db.Set<Aplicativo>();
        }

        [HttpGet, EnableQuery]
        public SingleResult<Aplicativo> Get([FromODataUri] string key)
        {
            return SingleResult.Create(
                db.Set<Aplicativo>().Where(x => x.Codigo == key));
        }

        [HttpPut]
        public IActionResult Put([FromODataUri] string key, Delta<Aplicativo> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Aplicativo t = db.Set<Aplicativo>().Where(e => e.Codigo == key ).SingleOrDefault();

            if (t == null)
            {
                return NotFound();
            }

            patch.Put(t);

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                if (!EntityExists(key))
                {
                    return NotFound();
                }
                else
                {
                    ModelState.AddModelError(ex.GetType().ToString(), ex.Message);
                    return BadRequest(ModelState);
                }
            }

            return Updated(t);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Aplicativo t)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Set<Aplicativo>().Add(t);

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                if (EntityExists(t.Codigo))
                {
                    return Conflict();
                }
                else
                {
                    ModelState.AddModelError(ex.GetType().ToString(), ex.Message);
                    return BadRequest(ModelState);
                }
            }

            return Created(t);
        }

        [AcceptVerbs("PATCH", "MERGE")]
        public IActionResult Patch([FromODataUri] string key, [FromBody] Delta<Aplicativo> patch)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Aplicativo t = db.Set<Aplicativo>().Where(e => e.Codigo == key).SingleOrDefault();

            if (t == null)
            {
                return NotFound();
            }

            patch.Patch(t);

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                if (!EntityExists(key))
                {
                    return NotFound();
                }
                else
                {
                    ModelState.AddModelError(ex.GetType().ToString(), ex.Message);
                    return BadRequest(ModelState);
                }
            }

            return Updated(t);
        }


        [HttpDelete]
        public IActionResult Delete([FromODataUri] string key)
        {
            Aplicativo t = db.Set<Aplicativo>().Where(e => e.Codigo == key).SingleOrDefault();

            if (t == null)
            {
                return NotFound();
            }

            try
            {
                db.Set<Aplicativo>().Remove(t);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(ex.GetType().ToString(), ex.Message);
                return BadRequest(ModelState);
            }


            return NoContent();
        }

        private bool EntityExists(string key)
        {
            return db.Set<Aplicativo>().Count(e => e.Codigo == key) > 0;
        }

        [HttpGet]
        [ODataRoute("GetTituloAplicativo(Codigo={Codigo})")]
        public IActionResult GetTituloAplicativo(string codigo)
        {
            var service = new AplicativoService(this.db);

            try
            {
                return Ok(service.getTitulo(codigo));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [ODataRoute("GetDescricaoAplicativo(Codigo={Codigo})")]
        public IActionResult GetDescricaoAplicativo(string codigo)
        {
            var service = new AplicativoService(this.db);

            try
            {
                return Ok(service.getDescricao(codigo));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
