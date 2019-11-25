using System;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.AppRepository.Services;
using IdxSistemas.Models;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class DocumentoRecebimentoController : BaseController<DocumentoRecebimento>
    {

        private readonly IConfiguration configuration;

        public DocumentoRecebimentoController(DataContext db, IConfiguration configuration) 
        {
            this.db = db;
            this.configuration = configuration;
        } 

        [HttpGet]
        [ODataRoute("GetNumeroDocumento(Data={data}, Loja={loja})")]
        public IActionResult GetNumeroDocumento(string data, string loja)
        {
            var service = new DocumentoRecebimentoService(this.db, this.configuration);
            try
            {
                return Ok(
                    service.getNumeroDocumento(DateTime.Parse(data), loja)
                );
            }
            catch (Exception)
            {
                throw;
            }
        }
        
    }
}