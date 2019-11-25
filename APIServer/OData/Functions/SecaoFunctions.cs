using IdxSistemas.DTO;
using IdxSistemas.Models;
using Microsoft.AspNet.OData.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdxSistemas.AppServer.OData.Functions
{
    public static class SecaoFunctions
    {
        public static void BuildFunctions(ODataConventionModelBuilder builder)
        {
            CalculaVendaSecao(builder);
            ExisteCodigoSecao(builder);
        }


        private static void ExisteCodigoSecao(ODataConventionModelBuilder builder)
        {
            var Function = builder.Function("ExisteCodigoSecao");
            Function.Returns<bool>();
            Function.Parameter<string>("Codigo");
        }
        

        private static void CalculaVendaSecao(ODataConventionModelBuilder builder)
        {
            var Function = builder.Function("CalculaVendaSecao");
            Function.Parameter<string>("LojaDe");
            Function.Parameter<string>("LojaAte");
            Function.Parameter<DateTime>("DataInicial");
            Function.Parameter<DateTime>("DataFinal");

            Function.Returns<string>();
        }
       
    }
}
