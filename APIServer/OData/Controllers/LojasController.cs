using System;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.Models;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class LojasController : BaseController<Loja>
    {
        public LojasController(DataContext db) => this.db = db;
    }
}