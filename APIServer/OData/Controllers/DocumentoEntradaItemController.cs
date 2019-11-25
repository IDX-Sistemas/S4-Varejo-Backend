using System;
using System.Linq;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class DocumentoEntradaItemController : BaseController<DocumentoEntradaItem>
    {
        public DocumentoEntradaItemController(DataContext db) => this.db = db;

        [HttpPut]
        public new IActionResult Put([FromODataUri] long key, Delta<DocumentoEntradaItem> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DocumentoEntradaItem t = db.DocumentoEntradaItem.Where( e => e.RowId == key && e.RowDeleted != "T").SingleOrDefault();

            if (t == null)
            {
                return NotFound();
            }

            patch.Put(t);
            
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

        [AcceptVerbs("PATCH", "MERGE")]
        public new IActionResult Patch([FromODataUri] long key, [FromBody] Delta<DocumentoEntradaItem> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DocumentoEntradaItem t = db.DocumentoEntradaItem.Where( e => e.RowId == key).SingleOrDefault();

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
                throw;
            }

            return Updated(t);
        }

         [HttpDelete]
        public new IActionResult Delete([FromODataUri] long key)
        {
            DocumentoEntradaItem t = db.DocumentoEntradaItem.Where( e => e.RowId == key && e.RowDeleted != "T").SingleOrDefault();
            
            if (t == null)
            {
                return NotFound();
            }

            try
            {
                db.DocumentoEntradaItem.Remove(t);
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
            return db.DocumentoEntradaItem.Count(e => e.RowId == key) > 0;
        }
    }
}