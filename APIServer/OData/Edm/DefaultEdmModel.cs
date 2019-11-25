using System;
using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;

using IdxSistemas.Models;
using IdxSistemas.DTO;
using IdxSistemas.AppServer.OData.Functions;

namespace IdxSistemas.AppServer.OData.Edm
{
    public static class DefaultEdmModel
    {
        public static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            
            #region ENTITY SETs

            builder.EntitySet<Agenda>("Agenda");
            builder.EntitySet<Cep>("Cep");
            builder.EntitySet<Produto>("Produtos");
            builder.EntitySet<Loja>("Lojas");
            builder.EntitySet<Cliente>("Clientes");
            builder.EntitySet<ContaBancaria>("ContaBancaria");
            builder.EntitySet<ContaPagar>("ContaPagar");
            builder.EntitySet<ContaReceber>("ContaReceber");
            builder.EntitySet<Despesa>("Despesas");
            builder.EntitySet<DocumentoEntrada>("DocumentoEntrada");
            builder.EntitySet<DocumentoEntradaItem>("DocumentoEntradaItem");
            builder.EntitySet<Fornecedor>("Fornecedores");
            builder.EntitySet<Funcionario>("Funcionarios");
            builder.EntitySet<Historico>("Historicos");
            builder.EntitySet<Marca>("Marcas");
            builder.EntitySet<MovimentoEstoque>("MovimentoEstoque");
            builder.EntitySet<Operadora>("Operadoras");
            builder.EntitySet<PedidoVenda>("PedidoVenda");
            builder.EntitySet<PedidoVendaItem>("PedidoVendaItem");
            builder.EntitySet<PreVenda>("PreVenda");
            builder.EntitySet<PreVendaItem>("PreVendaItem");
            builder.EntitySet<Produto>("Produtos");
            builder.EntitySet<RecebimentoConta>("RecebimentoConta");
            builder.EntitySet<SaldoEstoque>("SaldoEstoque");
            builder.EntitySet<Secao>("Secao");
            builder.EntitySet<Tabela>("Tabelas");
            builder.EntitySet<Tipo>("Tipos");
            builder.EntitySet<Usuario>("Usuarios");
            builder.EntitySet<Venda>("Vendas");
            builder.EntitySet<Vendedor>("Vendedores");
            builder.EntitySet<ContaCorrente>("ContaCorrente");
            builder.EntitySet<Parametro>("Parametros");
            builder.EntitySet<EntradaAntecipada>("EntradaAntecipada");
            builder.EntitySet<Cheque>("Cheques");
            builder.EntitySet<MovimentoCaixa>("MovimentoCaixa");
            builder.EntitySet<Cancelada>("Canceladas");
            builder.EntitySet<ContaReceberTemp>("ContaReceberTemp");
            builder.EntitySet<ContaPagarTemp>("ContaPagarTemp");
            builder.EntitySet<HistoricoCliente>("HistoricoCliente");
            builder.EntitySet<CondicaoPagamento>("CondicaoPagamento");
            builder.EntitySet<Relatorios>("Relatorios");
            builder.EntitySet<Pergunta>("Perguntas");
            builder.EntitySet<FechamentoCaixa>("FechamentoCaixa");
            
            #endregion

            EdmFunctions.BuildFunctions(builder);

            return builder.GetEdmModel();
        }
    }
}