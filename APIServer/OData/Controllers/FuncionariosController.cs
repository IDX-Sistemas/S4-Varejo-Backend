using System;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.Models;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class FuncionariosController : BaseController<Funcionario>
    {
        public FuncionariosController(DataContext db) => this.db = db;
    }
}