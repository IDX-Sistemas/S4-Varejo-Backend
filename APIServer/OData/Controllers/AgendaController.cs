using System;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.Models;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class AgendaController : BaseController<Agenda>
    {
        public AgendaController(DataContext db) => this.db = db;
    }
}