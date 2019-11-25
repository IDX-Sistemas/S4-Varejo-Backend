using System;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.Models;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class HistoricosController : BaseController<Historico>
    {
        public HistoricosController(DataContext db) => this.db = db;
    }
}