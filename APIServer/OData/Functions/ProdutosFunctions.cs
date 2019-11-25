using IdxSistemas.DTO;
using IdxSistemas.Models;
using Microsoft.AspNet.OData.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdxSistemas.AppServer.OData.Functions
{
    public static class ProdutosFunctions
    {
        public static void BuildFunctions(ODataConventionModelBuilder builder)
        {
            GetProdutoPeloCodigo(builder);
            ExisteCodigoProduto(builder);
            GetDescricaoProduto(builder);
        }


        private static void GetProdutoPeloCodigo(ODataConventionModelBuilder builder)
        {
            var FunctionGetProdutoPeloCodigo = builder.Function("GetProdutoPeloCodigo");
            FunctionGetProdutoPeloCodigo.ReturnsFromEntitySet<Produto>("Produtos");
            FunctionGetProdutoPeloCodigo.Parameter<string>("Codigo");
        }


        private static void ExisteCodigoProduto(ODataConventionModelBuilder builder)
        {
            var FunctionExisteCodigoProduto = builder.Function("ExisteCodigoProduto");
            FunctionExisteCodigoProduto.Returns<bool>();
            FunctionExisteCodigoProduto.Parameter<string>("Codigo");
        }


        private static void GetDescricaoProduto(ODataConventionModelBuilder builder)
        {
            var FunctionGetDescricaoProduto = builder.Function("GetDescricaoProduto");
            FunctionGetDescricaoProduto.Returns<string>();
            FunctionGetDescricaoProduto.Parameter<string>("Codigo");
        }
    }
}
