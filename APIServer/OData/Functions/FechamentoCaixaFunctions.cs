using IdxSistemas.DTO;
using IdxSistemas.Models;
using Microsoft.AspNet.OData.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdxSistemas.AppServer.OData.Functions
{
    public static class FechamentoCaixaFunctions
    {
        public static void BuildFunctions(ODataConventionModelBuilder builder)
        {
            FechamentoCaixa(builder);
        }


        private static void FechamentoCaixa(ODataConventionModelBuilder builder)
        {
            var Function = builder.Function("GeraFechamentoCaixa");
            Function.Parameter<string>("Loja");
            Function.Parameter<DateTime>("Data");
            Function.Returns<string>();
        }
    }
}
