using System;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.Models;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class TiposController : BaseController<Tipo>
    {
        public TiposController(DataContext db) => this.db = db;
    }
}