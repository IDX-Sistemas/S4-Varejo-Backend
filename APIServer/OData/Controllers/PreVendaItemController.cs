using IdxSistemas.AppRepository.Context;
using IdxSistemas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdxSistemas.AppServer.OData.Controllers 
{
    public class PreVendaItemController : BaseController<PreVendaItem>
    {
        public PreVendaItemController(DataContext db) => this.db = db;
    }
}
