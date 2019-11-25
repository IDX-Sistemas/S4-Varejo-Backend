using System;
using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;

using IdxSistemas.Models;

using IdxSistemas.AppServer.OData.Functions;

namespace IdxSistemas.AppServer.OData.Edm
{
    public static class PedidoVendaEdmModel
    {
        public static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();

            builder.EntitySet<Produto>("Produtos");
            builder.EntitySet<Loja>("Lojas");
            builder.EntitySet<Cliente>("Clientes");
            builder.EntitySet<PedidoVenda>("PedidoVenda");
            builder.EntitySet<PedidoVendaItem>("PedidoVendaItem");
            builder.EntitySet<PreVenda>("PreVenda");
            builder.EntitySet<PreVendaItem>("PreVendaItem");
            builder.EntitySet<Produto>("Produtos");
            builder.EntitySet<Vendedor>("Vendedores");
            builder.EntitySet<ContaReceberTemp>("ContaReceberTemp");
            builder.EntitySet<Operadora>("Operadoras");

            EdmFunctions.BuildFunctions(builder);

            return builder.GetEdmModel();
        }
    }
}