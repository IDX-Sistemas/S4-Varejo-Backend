using IdxSistemas.Models;
using Microsoft.AspNet.OData.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdxSistemas.AppServer.OData.Functions
{
    public static class RecebimentoContaFunctions
    {
        public static void BuildFunctions(ODataConventionModelBuilder builder)
        {
            RecebimentoTitulo(builder);
            EstornoRecebimento(builder);
            GetComprovanteRecebimento(builder);
        }


        private static void EstornoRecebimento(ODataConventionModelBuilder builder)
        {
            var Function = builder.Function("EstornoRecebimento");
            Function.Returns<int>();
            Function.Parameter<string>("NumeroDuplicata");
            Function.Parameter<string>("Loja");
            Function.Parameter<string>("NumeroDocumento");
            Function.Parameter<string>("DataPagamento");
        }


        private static void RecebimentoTitulo(ODataConventionModelBuilder builder)
        {
            var Function = builder.Function("RecebimentoTitulo");
            Function.Returns<int>();
            Function.Parameter<string>("NumeroDuplicata");
            Function.Parameter<DateTime>("DataPagamento");
            Function.Parameter<double>("ValorPago");
            Function.Parameter<string>("NumeroDocumento");
            Function.Parameter<string>("Loja");
            Function.Parameter<double>("ValorJuros");

            Function.Parameter<double>("ValorDesconto");
            Function.Parameter<int>("Quitar");
        }

        private static void GetComprovanteRecebimento(ODataConventionModelBuilder builder)
        {
            var Function = builder.Function("GetComprovanteRecebimento");
            Function.Parameter<string>("NumeroDocumento");
            Function.Parameter<string>("DataPagamento");
            Function.Parameter<string>("Loja");

            Function.Returns<string>();
        }
    }
}
