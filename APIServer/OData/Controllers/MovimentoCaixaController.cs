using System;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.Models;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class MovimentoCaixaController : BaseController<MovimentoCaixa>
    {
        public MovimentoCaixaController(DataContext db) => this.db = db;
    }
}