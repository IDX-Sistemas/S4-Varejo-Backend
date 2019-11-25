using System;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.AppRepository.Services;
using IdxSistemas.Models;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class ContaReceberTempController : BaseController<ContaReceberTemp>
    {
        private readonly IConfiguration configuration;
        
        public ContaReceberTempController(DataContext db, IConfiguration configuration) 
        {
            this.db = db;
            this.configuration = configuration;
        }

    }
}