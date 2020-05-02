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
    public class UsuarioFuncoesController : ODataController
    {
        private readonly DataContext db;

        public UsuarioFuncoesController(DataContext db) => this.db = db;

        [HttpGet, EnableQuery]
        public IQueryable<UsuarioFuncao> Get()
        {
            return db.Set<UsuarioFuncao>();
        }

        [HttpGet, EnableQuery]
        public SingleResult<UsuarioFuncao> Get([FromODataUri] string key)
        {
            return SingleResult.Create(
                db.Set<UsuarioFuncao>().Where(x => x.FuncaoId == key));
        }

        [HttpPut]
        public IActionResult Put([FromODataUri] string key, Delta<UsuarioFuncao> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UsuarioFuncao t = db.Set<UsuarioFuncao>().Where(e => e.FuncaoId == key ).SingleOrDefault();

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
        public IActionResult Post([FromBody] UsuarioFuncao t)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Set<UsuarioFuncao>().Add(t);

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                if (EntityExists(t.FuncaoId))
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
        public IActionResult Patch([FromODataUri] string key, [FromBody] Delta<UsuarioFuncao> patch)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UsuarioFuncao t = db.Set<UsuarioFuncao>().Where(e => e.FuncaoId == key).SingleOrDefault();

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
            UsuarioFuncao t = db.Set<UsuarioFuncao>().Where(e => e.FuncaoId == key).SingleOrDefault();

            if (t == null)
            {
                return NotFound();
            }

            try
            {
                db.Set<UsuarioFuncao>().Remove(t);
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
            return db.Set<UsuarioFuncao>().Count(e => e.FuncaoId == key) > 0;
        }
    }
}
