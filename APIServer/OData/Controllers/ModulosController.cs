using IdxSistemas.AppRepository.Context;
using IdxSistemas.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class ModulosController : ODataController
    {
        private readonly DataContext db;

        public ModulosController(DataContext db) => this.db = db;

        [HttpGet, EnableQuery]
        public IQueryable<Modulo> Get()
        {
            return db.Set<Modulo>();
        }

        [HttpGet, EnableQuery]
        public SingleResult<Modulo> Get([FromODataUri] string key)
        {
            return SingleResult.Create(
                db.Set<Modulo>().Where(x => x.Codigo == key));
        }

        [HttpPut]
        public IActionResult Put([FromODataUri] string key, Delta<Modulo> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            Modulo t = db.Set<Modulo>().Where(e => e.Codigo == key ).SingleOrDefault();

            if (t == null)
            {
                return NotFound();
            }

            patch.Put(t);

            if (string.IsNullOrEmpty(t.Sequencia))
            {
                ModelState.AddModelError("Sequencia", "Sequencia de exibicao não informada.");
                return BadRequest(ModelState);
            }

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
        public IActionResult Post([FromBody] Modulo t)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (string.IsNullOrEmpty(  t.Sequencia))
            {
                ModelState.AddModelError("Sequencia", "Sequencia de exibicao não informada.");
                return BadRequest(ModelState);
            }

            db.Set<Modulo>().Add(t);

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
        public IActionResult Patch([FromODataUri] string key, [FromBody] Delta<Modulo> patch)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Modulo t = db.Set<Modulo>().Where(e => e.Codigo == key).SingleOrDefault();

            if (t == null)
            {
                return NotFound();
            }

            patch.Patch(t);

            if (string.IsNullOrEmpty(t.Sequencia))
            {
                ModelState.AddModelError("Sequencia", "Sequencia de exibicao não informada.");
                return BadRequest(ModelState);
            }

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
            Modulo t = db.Set<Modulo>().Where(e => e.Codigo == key).SingleOrDefault();

            if (t == null)
            {
                return NotFound();
            }

            try
            {
                db.Set<Modulo>().Remove(t);
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
            return db.Set<Modulo>().Count(e => e.Codigo == key) > 0;
        }
    }
}
