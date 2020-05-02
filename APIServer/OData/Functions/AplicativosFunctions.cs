using Microsoft.AspNet.OData.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdxSistemas.AppServer.OData.Functions
{
    public class AplicativosFunctions
    {
        public static void BuildFunctions(ODataConventionModelBuilder builder)
        {
            GetTituloAplicativo(builder);
            GetDescricaoAplicativo(builder);
        }

        private static void GetTituloAplicativo(ODataConventionModelBuilder builder)
        {
            var Function = builder.Function("GetTituloAplicativo");
            Function.Parameter<string>("Codigo");
            Function.Returns<string>();
        }

        private static void GetDescricaoAplicativo(ODataConventionModelBuilder builder)
        {
            var Function = builder.Function("GetDescricaoAplicativo");
            Function.Parameter<string>("Codigo");
            Function.Returns<string>();
        }
    }
}
