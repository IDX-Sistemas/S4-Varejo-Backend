using System;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.Models;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class TabelasController : BaseController<Tabela>
    {
        public TabelasController(DataContext db) => this.db = db;
    }
}