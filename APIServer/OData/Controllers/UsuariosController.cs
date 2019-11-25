using System;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.Models;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class UsuariosController : BaseController<Usuario>
    {
        public UsuariosController(DataContext db) => this.db = db;
    }
}