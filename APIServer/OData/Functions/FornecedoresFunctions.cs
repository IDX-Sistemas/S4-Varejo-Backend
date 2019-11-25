using IdxSistemas.Models;
using Microsoft.AspNet.OData.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdxSistemas.AppServer.OData.Functions
{
    public static class FornecedoresFunctions
    {
        public static void BuildFunctions(ODataConventionModelBuilder builder)
        {
            GetProximoCodigoFornecedor(builder);
        }

        private static void GetProximoCodigoFornecedor(ODataConventionModelBuilder builder)
        {
            var function = builder.Function("GetProximoCodigoFornecedor");
            function.Returns<string>();
        }
   
    }
}
