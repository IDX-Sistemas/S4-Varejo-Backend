using System;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.AppRepository.Services;
using IdxSistemas.Models;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class ContaPagarTempController : BaseController<ContaPagarTemp>
    {
        private readonly IConfiguration configuration;
        
        public ContaPagarTempController(DataContext db, IConfiguration configuration) 
        {
            this.db = db;
            this.configuration = configuration;
        }

    }
}