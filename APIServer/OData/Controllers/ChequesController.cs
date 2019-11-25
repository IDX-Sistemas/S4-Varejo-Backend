using System;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.Models;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class ChequesController : BaseController<Cheque>
    {
        public ChequesController(DataContext db) => this.db = db;
    }
}