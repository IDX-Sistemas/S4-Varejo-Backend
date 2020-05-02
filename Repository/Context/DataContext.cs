using System;
using Microsoft.EntityFrameworkCore;

using IdxSistemas.Models;
using System.Linq;
using Repository.Context;

namespace IdxSistemas.AppRepository.Context
{
    public class DataContext : DbContext, IDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Loja> Lojas { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<ContaPagar> ContaPagar { get; set; }
        public DbSet<DocumentoEntrada> DocumentoEntrada { get; set; }
        public DbSet<DocumentoEntradaItem> DocumentoEntradaItem { get; set; }
        public DbSet<Secao> Secao { get; set; }
        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<ContaBancaria> ContasBancaria { get; set; }
        public DbSet<Historico> Historicos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Tabela> Tabelas { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Tipo> Tipos { get; set; }
        public DbSet<Agenda> Agendas { get; set; }
        public DbSet<Cep> Ceps { get; set; }
        public DbSet<ContaReceber> ContaReceber { get; set; }
        public DbSet<MovimentoEstoque> MovimentoEstoque { get; set; }
        public DbSet<SaldoEstoque> SaldoEstoque { get; set; }
        public DbSet<PreVenda> PreVenda { get; set; }
        public DbSet<PreVendaItem> PreVendaItem { get; set; }
        public DbSet<PedidoVenda> PedidoVenda { get; set; }
        public DbSet<PedidoVendaItem> PedidoVendaItem { get; set; }
        public DbSet<Operadora> Operadoras { get; set; }
        public DbSet<ContaCorrente> ContaCorrente { get; set; }
        public DbSet<Parametro> Parametros { get; set; }
        public DbSet<EntradaAntecipada> EntradaAntecipada {get; set;}
        public DbSet<Cheque> Cheques { get; set; }
        public DbSet<MovimentoCaixa> MovimentoCaixa { get; set; }
        public DbSet<Cancelada> Canceladas { get; set; }
        public DbSet<RecebimentoConta> RecebimentoConta {get; set; }
        public DbSet<DocumentoRecebimento> DocumentoRecebimento { get; set; }
        public DbSet<ContaReceberTemp> ContaReceberTemp { get; set; }
        public DbSet<HistoricoCliente> HistoricoCliente { get; set; }
        public DbSet<CondicaoPagamento> CondicaoPagamento { get; set; }
        public DbSet<ContaPagarTemp> ContaPagarTemp { get; set; }
        public DbSet<Relatorios> Relatorios { get; set; }
        public DbSet<Pergunta> Perguntas { get; set; }
        public DbSet<VendaSecao> VendaSecao { get; set; }
        public DbSet<FechamentoCaixa> FechamentoCaixa { get; set; }
        public DbSet<Modulo> Modulos { get; set; }
        public DbSet<Aplicativo> Aplicativos { get; set; }
        public DbSet<ModuloAplicativo> ModuloAplicativos { get; set; }
        public DbSet<UsuarioFuncao> UsuarioFuncao { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Loja>().HasKey(e => e.Codigo);

            builder.Entity<Cliente>().HasKey(e => e.Codigo);
            builder.Entity<Cliente>(e => e.HasIndex(c => c.Codigo).IsUnique(true));

            builder.Entity<Fornecedor>().HasKey(e => e.Codigo);
            builder.Entity<Fornecedor>(e => e.HasIndex(c => c.Codigo).IsUnique(true));

            builder.Entity<Produto>().HasKey(e => e.Codigo);
            builder.Entity<Produto>(e => e.HasIndex(c => c.Codigo).IsUnique(true));

            builder.Entity<ContaBancaria>().HasKey(e => e.Codigo);

            builder.Entity<Vendedor>().HasKey(e => e.Codigo);

            builder.Entity<DocumentoEntrada>().HasKey(e => new { e.Numero, e.Fornecedor });
            builder.Entity<DocumentoEntrada>()
                .HasMany<DocumentoEntradaItem>(m => m.DocumentoEntradaItems)
                .WithOne(e => e.DocumentoEntrada)
                .HasForeignKey(e => new { e.DocumentoEntradaNumero, e.Fornecedor });

            builder.Entity<DocumentoEntradaItem>().HasKey(e => new { e.DocumentoEntradaNumero, e.Fornecedor, e.RowId });

            builder.Entity<PedidoVenda>().HasKey(e => new { e.Numero, e.Loja });
            builder.Entity<PedidoVenda>()
                .HasMany<PedidoVendaItem>(m => m.PedidoVendaItems)
                .WithOne(e => e.PedidoVenda)
                .HasForeignKey(e => new { e.NumeroVenda, e.Loja });

            builder.Entity<PedidoVendaItem>().HasKey(e => new { e.NumeroVenda, e.Loja, e.RowId });

            builder.Entity<PreVenda>().HasKey(e => e.Numero);

            builder.Entity<CondicaoPagamento>().HasKey(e => e.Codigo);

            builder.Entity<ContaReceber>().HasKey(e => new { e.NumeroDuplicata, e.Loja });

            builder.Entity<FechamentoCaixa>(e => e.HasIndex(c => c.Data).IsUnique(false));

        }
    }
}