using IdxSistemas.DTO;
using IdxSistemas.Models;
using Microsoft.AspNet.OData.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdxSistemas.AppServer.OData.Functions
{
    public static class HistoricoClienteFunctions
    {
        public static void BuildFunctions(ODataConventionModelBuilder builder)
        {
            GetHistoricoCliente(builder);
        }


        private static void GetHistoricoCliente(ODataConventionModelBuilder builder)
        {
            var Function = builder.Function("GetHistoricoCliente");
            Function.Parameter<string>("Codigo");
            Function.ReturnsCollectionFromEntitySet<HistoricoCliente>("HistoricoCliente");
            
        }

    }
}
