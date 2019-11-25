using IdxSistemas.AppRepository.Context;
using IdxSistemas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class CondicaoPagamentoController :  BaseController<CondicaoPagamento>
    {
        public CondicaoPagamentoController(DataContext db)
        {
            this.db = db;
        }
    }
}
