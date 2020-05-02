using IdxSistemas.DTO;
using IdxSistemas.Models;
using Microsoft.AspNet.OData.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdxSistemas.AppServer.OData.Functions
{
    public static class EdmFunctions
    {
        public static void BuildFunctions(ODataConventionModelBuilder builder)
        {
            ClientesFunctions.BuildFunctions(builder);
            FornecedoresFunctions.BuildFunctions(builder);
            PedidoVendaFunctions.BuildFunctions(builder);
            RecebimentoContaFunctions.BuildFunctions(builder);
            HistoricoClienteFunctions.BuildFunctions(builder);
            ProdutosFunctions.BuildFunctions(builder);
            DocumentoEntradaFunctions.BuildFunctions(builder);
            PerguntasFunctions.BuildFunctions(builder);
            RelatoriosFunctions.BuildFunctions(builder);
            SecaoFunctions.BuildFunctions(builder);
            FechamentoCaixaFunctions.BuildFunctions(builder);
            AplicativosFunctions.BuildFunctions(builder);


            var FunctionExisteCodigoVendedor = builder.Function("ExisteCodigoVendedor");
            FunctionExisteCodigoVendedor.Returns<bool>();
            FunctionExisteCodigoVendedor.Parameter<string>("Codigo");

            var FunctionGetPrecoVistaProduto = builder.Function("GetPrecoVistaProduto");
            FunctionGetPrecoVistaProduto.Returns<double>();
            FunctionGetPrecoVistaProduto.Parameter<string>("Codigo");

            var FunctionGetItemsDocumentoEntrada = builder.Function("GetItemsDocumentoEntrada");
            FunctionGetItemsDocumentoEntrada.ReturnsCollection<string>();
            FunctionGetItemsDocumentoEntrada.Parameter<string>("Numero");
            FunctionGetItemsDocumentoEntrada.Parameter<string>("Fornecedor");

            var FunctionLimpaItemsDocumentoEntrada = builder.Function("LimpaItemsDocumentoEntrada");
            FunctionLimpaItemsDocumentoEntrada.Returns<int>();
            FunctionLimpaItemsDocumentoEntrada.Parameter<string>("Numero");
            FunctionLimpaItemsDocumentoEntrada.Parameter<string>("Fornecedor");


            var FunctionGetContasReceberPorCliente = builder.Function("GetContasReceberPorCliente");
            FunctionGetContasReceberPorCliente.ReturnsCollection<ContaReceberDTO>();
            FunctionGetContasReceberPorCliente.Parameter<string>("Codigo");
            FunctionGetContasReceberPorCliente.Parameter<string>("Database");


            var FunctionGetNumeroDocumento = builder.Function("GetNumeroDocumento");
            FunctionGetNumeroDocumento.Returns<string>();
            FunctionGetNumeroDocumento.Parameter<string>("Data");
            FunctionGetNumeroDocumento.Parameter<string>("Loja");

            var FunctionExistePedidoVenda = builder.Function("ExistePedidoVenda");
            FunctionExistePedidoVenda.Returns<bool>();
            FunctionExistePedidoVenda.Parameter<string>("Numero");
            FunctionExistePedidoVenda.Parameter<string>("Loja");


            var FunctionPedidoVendaRecebido = builder.Function("PedidoVendaRecebido");
            FunctionPedidoVendaRecebido.Returns<bool>();
            FunctionPedidoVendaRecebido.Parameter<string>("Numero");
            FunctionPedidoVendaRecebido.Parameter<string>("Loja");

            var FunctionAjustaVencimentoContaReceber = builder.Function("AjustaVencimentoContaReceber");
            FunctionAjustaVencimentoContaReceber.Returns<bool>();
            FunctionAjustaVencimentoContaReceber.Parameter<long>("Id");
            FunctionAjustaVencimentoContaReceber.Parameter<string>("Data");
            FunctionAjustaVencimentoContaReceber.Parameter<double>("Valor");

            var FunctionAtualizaStatusPreVenda = builder.Function("AtualizaStatusPreVenda");
            FunctionAtualizaStatusPreVenda.Returns<int>();
            FunctionAtualizaStatusPreVenda.Parameter<string>("Numero");
            FunctionAtualizaStatusPreVenda.Parameter<string>("Status");
        }
    }
}
