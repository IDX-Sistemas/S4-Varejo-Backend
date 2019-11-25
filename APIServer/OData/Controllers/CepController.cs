using System;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.Models;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class CepController : BaseController<Cep>
    {
        public CepController(DataContext db) => this.db = db;
    }
}