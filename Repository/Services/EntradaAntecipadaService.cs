using System;
using System.Data.SqlClient;
using System.Linq;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.Models;
using Microsoft.Extensions.Configuration;

namespace IdxSistemas.AppRepository.Services
{
    public class EntradaAntecipadaService
    {

        private readonly DataContext db;

        private readonly string connString = "";

        private readonly IConfiguration configuration;

        private readonly SaldoEstoqueService saldoEstoqueService;

        private readonly DocumentoEntradaService documentoEntradaService;

        public EntradaAntecipadaService(DataContext db,IConfiguration configuration){ 
            this.db = db;
            this.configuration = configuration;

            this.connString = configuration.GetConnectionString("sage_db");
            this.saldoEstoqueService = new SaldoEstoqueService(db, configuration);
            this.documentoEntradaService = new DocumentoEntradaService(db, configuration);
        }

        public int EntradaAntecipada(EntradaAntecipada entrada)
        {
            try
            {
                var produto = db.Produtos.Where(e => e.Codigo == entrada.Produto && e.RowDeleted != "T").SingleOrDefault();
                
                if (produto == null)
                {
                    produto = new Produto
                    {
                        Codigo = entrada.Produto,
                        Descricao = entrada.DescricaoProduto,
                        DescricaoEtiqueta1 = entrada.DescricaoEtiqueta1,
                        DescricaoEtiqueta2 = entrada.DescricaoEtiqueta2,
                        Unidade = "PC",
                        ValorVista = Math.Round( (double)entrada.PrecoVista, 2),
                        ValorPrazo = Math.Round( (double)entrada.PrecoPrazo, 2),
                        ValorCusto = Math.Round( (double)entrada.PrecoCusto, 2),
                        Status = "A",
                        Fornecedor = entrada.Fornecedor,
                        UltimaCompra = entrada.DataEntrada,
                        Secao = entrada.Secao
                    };
                    
                    db.Produtos.Add(produto);
                }
                else
                {
                    produto.Descricao = entrada.DescricaoProduto;
                    produto.DescricaoEtiqueta1 = entrada.DescricaoEtiqueta1;
                    produto.DescricaoEtiqueta2 = entrada.DescricaoEtiqueta2;
                    produto.ValorVista = Math.Round( (double)entrada.PrecoVista, 2);
                    produto.ValorPrazo = Math.Round( (double)entrada.PrecoPrazo, 2);
                    produto.ValorCusto = Math.Round( (double)entrada.PrecoCusto, 2);
                    produto.Fornecedor = entrada.Fornecedor;
                    produto.UltimaCompra = entrada.DataEntrada;
                    produto.Secao = entrada.Secao;

                    db.Produtos.Update(produto);
                }

                var item = new DocumentoEntradaItem
                {
                    Codigo = entrada.Produto,
                    DocumentoEntradaNumero = entrada.NotaFiscal,
                    Classificacao = "",
                    DataEntrada = (DateTime) entrada.DataEntrada,
                    Fornecedor = entrada.Fornecedor,
                    Loja = entrada.Loja,
                    Quantidade = (int) entrada.Quantidade,
                    ValorUnitario = Math.Round( (double) entrada.PrecoCusto, 2),
                    EntradaAntecipada = "S"
                };

                db.DocumentoEntradaItem.Add(item);
                db.SaveChanges();

                saldoEstoqueService.EntradaItem(item.Codigo, item.Loja, item.Quantidade);

                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public void EstornoEntradaAntecipada(EntradaAntecipada entrada)
        {
            try
            {
                if ( documentoEntradaService.ExisteDocumentoEntrada(entrada.NotaFiscal, entrada.Fornecedor) )
                {
                    throw new Exception("Documento de Entrada/Nota Fiscal já lancada.");
                }

                var item = db.DocumentoEntradaItem
                                .Where(e => e.Codigo == entrada.Produto && e.DocumentoEntradaNumero == entrada.NotaFiscal &&
                                            e.Fornecedor == entrada.Fornecedor && e.Loja == entrada.Loja && e.RowDeleted != "T")
                                .SingleOrDefault();

                if (item != null)
                {
                    db.DocumentoEntradaItem.Remove(item);
                    db.SaveChanges();
                }

                saldoEstoqueService.SaidaItem(entrada.Produto, entrada.Loja, (int)entrada.Quantidade);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

    }
}