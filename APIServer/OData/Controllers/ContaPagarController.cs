using System;
using System.Linq;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class ContaPagarController : BaseController<ContaPagar>
    {
        public ContaPagarController(DataContext db) => this.db = db;

       /*  [HttpGet, EnableQuery]
        public new IQueryable<ContaPagar> Get()
        {
            return db.ContaPagar
                .Where( e => !(e.Fornecedor.CompareTo("1000") >=0 && e.Fornecedor.CompareTo("2000") <=0 ) && e.RowDeleted != "T");
        } */
    }
}