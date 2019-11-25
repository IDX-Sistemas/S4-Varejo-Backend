using IdxSistemas.AppRepository.Context;
using IdxSistemas.Models;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class RelatoriosController : BaseController<Relatorios>
    {
        public RelatoriosController(DataContext db) => this.db = db;

        [HttpPost]
        public new IActionResult Post([FromBody] Relatorios t)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (ExisteCodigo(t.Codigo))
            {
                ModelState.AddModelError("Codigo", "Ja existe chave Codigo");
                return BadRequest(ModelState);
            }

            db.Set<Relatorios>().Add(t);

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

        private bool EntityExists(long key)
        {
            return db.Set<Relatorios>().Count(e => e.RowId == key) > 0;
        }


        private bool ExisteCodigo(string codigo)
        {
            return db.Relatorios
                .Count(e => e.Codigo == codigo && e.RowDeleted != "T") > 0;
        }


        [HttpGet]
        [ODataRoute("ProximoCodigoRelatorio()")]
        public IActionResult ProximoCodigoRelatorio()
        {
            var ultimoCodigo = this.db.Relatorios.Where(e => e.RowDeleted != "T").Max(e => e.Codigo);

            if (ultimoCodigo == "")
                ultimoCodigo = "00000";

            var proximoCodigo = (Convert.ToInt32(ultimoCodigo) + 1).ToString().PadLeft(5, '0');

            return Ok(proximoCodigo);
        }

    }
}
