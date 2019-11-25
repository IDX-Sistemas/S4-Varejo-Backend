using System;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.Models;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class OperadorasController : BaseController<Operadora>
    {
        public OperadorasController(DataContext db) => this.db = db;
    }
}