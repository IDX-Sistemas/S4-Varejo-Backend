using System;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.Models;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class MarcasController : BaseController<Marca>
    {
        public MarcasController(DataContext db) => this.db = db;
    }
}