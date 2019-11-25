using IdxSistemas.Models;
using Microsoft.AspNet.OData.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdxSistemas.AppServer.OData.Functions
{
    public static class DocumentoEntradaFunctions
    { 
        public static void BuildFunctions(ODataConventionModelBuilder builder)
        {
            ClassificaDocumentoEntrada(builder);
            EstornaClassificacaoDocumentoEntrada(builder);
        }


        private static void ClassificaDocumentoEntrada(ODataConventionModelBuilder builder)
        {
            var Function = builder.Function("ClassificaDocumentoEntrada");
            Function.Parameter<string>("Numero");
            Function.Parameter<string>("Fornecedor");
            Function.Returns<int>();
        }


        private static void EstornaClassificacaoDocumentoEntrada(ODataConventionModelBuilder builder)
        {
            var Function = builder.Function("EstornaClassificacaoDocumentoEntrada");
            Function.Parameter<string>("Numero");
            Function.Parameter<string>("Fornecedor");
            Function.Returns<int>();
        }


    }
}
