using System;
using System.Linq;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class ContaBancariaController : BaseController<ContaBancaria>
    {
        public ContaBancariaController(DataContext db) => this.db = db;


        [AcceptVerbs("PATCH", "MERGE")]
        public new IActionResult Patch([FromODataUri] long key, [FromBody] Delta<ContaBancaria> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ContaBancaria t = db.ContasBancaria.Where( e => e.RowId == key && e.RowDeleted != "T").SingleOrDefault();
            
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
            ContaBancaria t = db.ContasBancaria.Where( e => e.RowId == key && e.RowDeleted != "T").SingleOrDefault();

            if (t == null)
            {
                return NotFound();
            }

            try
            {
                db.ContasBancaria.Remove(t);
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
            return db.ContasBancaria.Count(e => e.RowId == key) > 0;
        }
    }
}