using System;
using System.Linq;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.AppRepository.Services;
using IdxSistemas.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class DocumentoEntradaController : BaseController<DocumentoEntrada>
    {
        private readonly IConfiguration configuration;
        private readonly DocumentoEntradaService service;

        public DocumentoEntradaController(DataContext db, IConfiguration configuration)
        {
            this.db = db;
            this.configuration = configuration;
            this.service = new DocumentoEntradaService(db, configuration);
        }

        [HttpPost]
        public new IActionResult Post([FromBody] DocumentoEntrada t)
        {
            if ( string.IsNullOrEmpty( t.Condicao ))
            {
                ModelState.AddModelError("CondicaoPagamento", "Condicao de pagamento nao informada.");
            }

            if (string.IsNullOrEmpty(t.NumeroDuplicata))
            {
                ModelState.AddModelError("NumeroDuplicata", "Numero Duplicata nao informada.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DocumentoEntrada.Add(t);

            try
            {
                using (var tr = db.Database.BeginTransaction() )
                {
                    db.SaveChanges();
                    service.CriaContaPagarTemp(t.Numero, t.Fornecedor);

                    tr.Commit();
                }
               
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
        public new IActionResult Patch([FromODataUri] long key, [FromBody] Delta<DocumentoEntrada> patch)
        {
         
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DocumentoEntrada t = db.DocumentoEntrada.Where( e => e.RowId == key).SingleOrDefault();

            if (t == null)
            {
                return NotFound();
            }

            patch.Patch(t);
           
            try
            {
                using (var tr = db.Database.BeginTransaction())
                {
                    db.SaveChanges();
                    service.CriaContaPagarTemp(t.Numero, t.Fornecedor);
                    tr.Commit();
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ModelState.AddModelError(ex.GetType().ToString(), ex.Message);
                return BadRequest(ModelState);
            }

            return Updated(t);
        }

        public new IActionResult Delete([FromODataUri] long key)
        {
            DocumentoEntrada t = db.DocumentoEntrada.Where( e => e.RowId == key).SingleOrDefault();

            if (t == null)
            {
                return NotFound();
            }

            try
            {
                var items = db.DocumentoEntradaItem
                    .Where( e => e.DocumentoEntradaNumero == t.Numero && e.Fornecedor == t.Fornecedor && e.EntradaAntecipada != "S" && t.RowDeleted != "T");
                
                foreach (var item in items)
                {
                    db.DocumentoEntradaItem.Remove(item);
                }

                //deletar conta pagar temp
                // deletar conta pagar se estiver em aberto

                db.DocumentoEntrada.Remove(t);
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ModelState.AddModelError(ex.GetType().ToString(), ex.Message);
                return BadRequest(ModelState);
            }
           
            return NoContent();
        }
        

        private bool EntityExists(long key)
        {
            return db.DocumentoEntrada.Count(e => e.RowId == key) > 0;
        }


        [HttpGet]
        [ODataRoute("GetItemsDocumentoEntrada(Numero={numero}, Fornecedor={fornecedor})")]
        public IActionResult GetItemsDocumentoEntrada(string numero, string fornecedor)
        {
            var service = new DocumentoEntradaService(this.db, this.configuration);
            try
            {
                return Ok(service.getItems(numero, fornecedor));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [ODataRoute("LimpaItemsDocumentoEntrada(Numero={numero}, Fornecedor={fornecedor})")]
        public IActionResult LimpaItemsDocumentoEntrada(string numero, string fornecedor)
        {
            var service = new DocumentoEntradaService(this.db, this.configuration);
            try
            {
                return Ok(service.clearItems(numero, fornecedor));
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet]
        [ODataRoute("ClassificaDocumentoEntrada(Numero={numero}, Fornecedor={fornecedor})")]
        public IActionResult ClassificaDocumentoEntrada(string numero, string fornecedor)
        {
            var service = new DocumentoEntradaService(this.db, this.configuration);
            try
            {
                return Ok(service.ClassificarDocumento(numero, fornecedor));
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet]
        [ODataRoute("EstornaClassificacaoDocumentoEntrada(Numero={numero}, Fornecedor={fornecedor})")]
        public IActionResult EstornaClassificacaoDocumentoEntrada(string numero, string fornecedor)
        {
            var service = new DocumentoEntradaService(this.db, this.configuration);
            try
            {
                return Ok(service.EstornaClassificacaoDocumentoEntrada(numero, fornecedor));
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}