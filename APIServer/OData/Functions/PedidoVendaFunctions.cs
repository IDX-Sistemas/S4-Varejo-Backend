using IdxSistemas.Models;
using Microsoft.AspNet.OData.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdxSistemas.AppServer.OData.Functions
{
    public static class PedidoVendaFunctions
    {
        public static void BuildFunctions(ODataConventionModelBuilder builder)
        {
            FaturaPedido(builder);
            EstornaFaturamentoPedido(builder);
            CancelaPedido(builder);
            GetDadosCarne(builder);
        }


        private static void FaturaPedido(ODataConventionModelBuilder builder)
        {
            var Function = builder.Function("FaturaPedido");
            Function.Parameter<string>("Numero");
            Function.Parameter<string>("Loja");
            Function.Returns<int>();
        }


        private static void EstornaFaturamentoPedido(ODataConventionModelBuilder builder)
        {
            var Function = builder.Function("EstornaFaturamentoPedido");
            Function.Parameter<string>("Numero");
            Function.Parameter<string>("Loja");
            Function.Returns<int>();
        }

        private static void CancelaPedido(ODataConventionModelBuilder builder)
        {
            var Function = builder.Function("CancelaPedido");
            Function.Parameter<string>("Numero");
            Function.Parameter<string>("Loja");
            Function.Returns<int>();
        }

        private static void GetDadosCarne(ODataConventionModelBuilder builder)
        {
            var Function = builder.Function("GetDadosCarne");
            Function.Parameter<string>("Numero");
            Function.Parameter<string>("Loja");
            Function.Returns<string>();
        }
    }
}
