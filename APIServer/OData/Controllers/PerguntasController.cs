using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using IdxSistemas.Models;

using IdxSistemas.AppRepository.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.OData.Routing;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class PerguntasController : BaseController<Pergunta>
    {
        public PerguntasController(DataContext db) => this.db = db;


        [HttpPost]
        public new IActionResult Post([FromBody] Pergunta t)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if ( ExisteOrdem(t.Codigo, t.Ordem) )
            {
                ModelState.AddModelError("Ordem", "Ja existe chave codigo/ordem");
                return BadRequest(ModelState);
            }

            db.Set<Pergunta>().Add(t);

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
            return db.Set<Pergunta>().Count(e => e.RowId == key) > 0;
        }


        private bool ExisteOrdem(string codigo, string ordem)
        {
            return db.Perguntas
                .Count( e => e.Codigo == codigo && e.Ordem == ordem && e.RowDeleted != "T") > 0;
        }



        [HttpGet]
        [ODataRoute("ProximaOrdemPergunta(Codigo={codigo})")]
        public IActionResult ProximaOrdemPergunta(string codigo)
        {
            var ultimaOrdem = this.db.Perguntas.Where(e => e.Codigo == codigo && e.RowDeleted != "T").Max(e => e.Ordem);

            if (ultimaOrdem == "")
                ultimaOrdem = "00";

            var proximaOrdem =  (Convert.ToInt32(ultimaOrdem) + 1).ToString().PadLeft(2,'0')   ;

            return Ok(proximaOrdem);
        }
    }
}
