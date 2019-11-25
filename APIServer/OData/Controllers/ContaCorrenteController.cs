using System;
using System.Linq;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.AppRepository.Services;
using IdxSistemas.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class ContaCorrenteController : BaseController<ContaCorrente>
    {
        private readonly IConfiguration configuration;
        private readonly ContaCorrenteService service;


        public ContaCorrenteController(DataContext db, IConfiguration configuration)
        {
            this.db = db;
            this.configuration = configuration;
            this.service = new ContaCorrenteService(db, configuration);
        }

        [HttpPost]
        public new IActionResult Post([FromBody] ContaCorrente t)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ContaCorrente.Add(t);

            try
            {
                db.SaveChanges();
                service.BaixaDuplicataPagar(t.CodigoFornecedor, t.NumeroDuplicata, t.Data, t.Valor, t.CodigoConta, t.Cheque);
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
        public new IActionResult Patch([FromODataUri] long key, [FromBody] Delta<ContaCorrente> patch)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ContaCorrente t = db.ContaCorrente.Where(e => e.RowId == key && e.RowDeleted != "T").SingleOrDefault();

            if (t == null)
            {
                return NotFound();
            }

            patch.Patch(t);

            try
            {
                db.SaveChanges();
                service.BaixaDuplicataPagar(t.CodigoFornecedor, t.NumeroDuplicata, t.Data, t.Valor, t.CodigoConta, t.Cheque);
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
        public new IActionResult Delete([FromODataUri] long key)
        {
            ContaCorrente t = db.ContaCorrente.Where(e => e.RowId == key && e.RowDeleted != "T").SingleOrDefault();

            if (t == null)
            {
                return NotFound();
            }

            try
            {
                db.ContaCorrente.Remove(t);
                db.SaveChanges();

                service.EstornaBaixaDuplicataPagar(t.CodigoFornecedor, t.NumeroDuplicata);
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
            return db.ContaCorrente.Count(e => e.RowId == key) > 0;
        }
    }
    
}