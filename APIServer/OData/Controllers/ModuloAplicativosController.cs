using IdxSistemas.AppRepository.Context;
using IdxSistemas.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class ModuloAplicativosController : BaseController<ModuloAplicativo>
    {
        public ModuloAplicativosController(DataContext db) => this.db = db;

    }
}
