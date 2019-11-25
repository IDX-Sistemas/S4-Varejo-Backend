using System;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.Models;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class DespesasController : BaseController<Despesa>
    {
        public DespesasController(DataContext db) => this.db = db;
    }
}