using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using IdxSistemas.AppRepository.Context;
using IdxSistemas.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.OData;

namespace IdxSistemas.AppServer.OData.Controllers
{

    public class BaseController<T> : ODataController
        where T : BaseModel
    {
        public DataContext db;

        [HttpGet, EnableQuery]
        public IQueryable<T> Get()
        {
            return db.Set<T>().Where( e => e.RowDeleted != "T");
        }

        [HttpGet, EnableQuery]
        public SingleResult<T> Get([FromODataUri] long key)
        {
            return SingleResult.Create(
                db.Set<T>().Where(x => x.RowId == key && x.RowDeleted != "T"));
        }

        [HttpPut]
        public IActionResult Put([FromODataUri] long key, Delta<T> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            T t = db.Set<T>().Where( e => e.RowId == key && e.RowDeleted != "T").SingleOrDefault();

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
        public IActionResult Post([FromBody] T t)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Set<T>().Add(t);

            try
            {
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
                    ModelState.AddModelError(ex.GetType().ToString(), ex.Message);
                    return BadRequest(ModelState);
                }
            }

            return Created(t);
        }

        [AcceptVerbs("PATCH", "MERGE")]
        public IActionResult Patch([FromODataUri] long key, [FromBody] Delta<T> patch)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            T t = db.Set<T>().Where( e => e.RowId == key && e.RowDeleted != "T").SingleOrDefault();
            
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
        public IActionResult Delete([FromODataUri] long key)
        {
            T t = db.Set<T>().Where( e => e.RowId == key && e.RowDeleted != "T").SingleOrDefault();
            
            if (t == null)
            {
                return NotFound();
            }

            try
            {
                db.Set<T>().Remove(t);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(ex.GetType().ToString(), ex.Message);
                return BadRequest(ModelState);
            }
           

            return NoContent();
        }

        private bool EntityExists(long key)
        {
            return db.Set<T>().Count(e => e.RowId == key) > 0;
        }
    }
}
