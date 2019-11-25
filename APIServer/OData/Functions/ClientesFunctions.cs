using IdxSistemas.DTO;
using IdxSistemas.Models;
using Microsoft.AspNet.OData.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdxSistemas.AppServer.OData.Functions
{
    public static class ClientesFunctions
    {
        public static void BuildFunctions(ODataConventionModelBuilder builder)
        {
            GetProximoCodigoCliente(builder);
            GetClientePeloCodigo(builder);
            GetHistoricoPorCliente(builder);
        }


        private static void GetProximoCodigoCliente(ODataConventionModelBuilder builder)
        {
            var Function = builder.Function("GetProximoCodigoCliente");
            Function.Returns<string>();
        }


        private static void GetClientePeloCodigo(ODataConventionModelBuilder builder)
        {
            var Function = builder.Function("GetClientePeloCodigo");
            Function.ReturnsFromEntitySet<Cliente>("Clientes");
            Function.Parameter<string>("Codigo");
        }


        private static void GetHistoricoPorCliente(ODataConventionModelBuilder builder)
        {
            var Function = builder.Function("GetHistoricoPorCliente");
            Function.ReturnsCollection<ContaReceberDTO>();
            Function.Parameter<string>("Codigo");
        }
    }
}
