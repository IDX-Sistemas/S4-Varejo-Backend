using System;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.Models;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class ParametrosController : BaseController<Parametro>
    {
        public ParametrosController(DataContext db) => this.db = db;
    }
    
}