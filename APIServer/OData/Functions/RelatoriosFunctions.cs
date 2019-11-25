using IdxSistemas.DTO;
using IdxSistemas.Models;
using Microsoft.AspNet.OData.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdxSistemas.AppServer.OData.Functions
{
    public static class RelatoriosFunctions
    {
        public static void BuildFunctions(ODataConventionModelBuilder builder)
        {
            ProximoCodigoRelatorio(builder);
        }


        private static void ProximoCodigoRelatorio(ODataConventionModelBuilder builder)
        {
            var Function = builder.Function("ProximoCodigoRelatorio");
            Function.Returns<string>();
        }
       
    }
}
