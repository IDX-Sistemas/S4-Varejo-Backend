using System;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.Models;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class SaldoEstoqueController : BaseController<SaldoEstoque>
    {
        public SaldoEstoqueController(DataContext db) => this.db = db;
    }
}