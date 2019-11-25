using IdxSistemas.DTO;
using IdxSistemas.Models;
using Microsoft.AspNet.OData.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdxSistemas.AppServer.OData.Functions
{
    public static class PerguntasFunctions
    {
        public static void BuildFunctions(ODataConventionModelBuilder builder)
        {
            ProximaOrdemPergunta(builder);
        }


        private static void ProximaOrdemPergunta(ODataConventionModelBuilder builder)
        {
            var Function = builder.Function("ProximaOrdemPergunta");
            Function.Parameter<string>("Codigo");
            Function.Returns<string>();
        }
       
    }
}
