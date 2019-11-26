using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.Models;
using Microsoft.Extensions.Configuration;

namespace IdxSistemas.AppRepository.Services
{
    public class FechamentoCaixaService
    {

        private readonly DataContext db;
  
        public FechamentoCaixaService(DataContext db)
        {
            this.db = db;
        }

        public void GeraFechamentoCaixa(string loja, DateTime data)
        {
            double? valorVista = 0;
            double? valorEntrada = 0;
            double? valorCrediario = 0;
            double? valorPrazo = 0;
            double? valorCheque = 0;
            double? valorJuros = 0;
            double? valorAcrescimo = 0;
            double? valorDesconto = 0;
            double? valorDespesa = 0;

            FechamentoCaixa fechamento = db.FechamentoCaixa
                   .Where(e => e.Loja == loja && e.Data == data && e.RowDeleted != "T")
                   .SingleOrDefault();

            db.ContaReceber
                .Where(e => e.Loja == loja && e.DataEmissao == data && e.RowDeleted != "T")
                .ToList()
                .ForEach(e => { 
                
                    if (e.TipoVenda == "1")
                    {
                        valorVista += e.ValorDuplicata;
                    }
                    else if(e.TipoVenda == "4")
                    {
                        if (e.FlagEntrada == "S")
                        {
                            valorEntrada += e.ValorDuplicata;
                        }
                        else
                        {
                            valorCheque += e.ValorDuplicata;
                        }
                    }
                    else
                    {
                        if (e.FlagEntrada == "S")
                        {
                            valorEntrada += e.ValorDuplicata;
                        }
                        else
                        {
                            valorPrazo += e.ValorDuplicata;
                        }
                    }
                
                });


            db.RecebimentoConta
                .Where(e => e.DataPagamento == data && e.Loja == loja && e.RowDeleted != "T")
                .ToList()
                .ForEach(e => {

                    if (e.ContaReceber.TipoVenda == "2" || e.ContaReceber.TipoVenda == "3")
                    {
                        if (e.ContaReceber.FlagEntrada != "S")
                            valorCrediario += e.ValorPago;
                    }
                    
                    valorJuros += e.ValorJuros;
                });


            db.PedidoVenda
                 .Where(e => e.Data == data && e.Loja == loja && e.RowDeleted != "T")
                 .ToList()
                 .ForEach(e => {
                     valorAcrescimo += e.ValorAcrescimo;
                     valorDesconto += e.ValorDesconto;
                 });


            db.ContaPagar
                .Where(e => e.DataPagamento == data && e.Loja == loja && e.Fornecedor == "2000" && e.RowDeleted != "T")
                .ToList()
                .ForEach(e => {
                    valorDespesa += e.ValorPago;
                });


            if (fechamento == null)
            {
                fechamento = new FechamentoCaixa
                {
                    Loja = loja,
                    Data = data,
                    ValorAcrescimo = valorAcrescimo,
                    ValorCheque = valorCheque,
                    ValorCrediario = valorCrediario,
                    ValorDesconto = valorDesconto,
                    ValorDespesa = valorDespesa,
                    ValorEntrada = valorEntrada,
                    ValorJuros = valorJuros,
                    ValorPrazo = valorPrazo,
                    ValorVista = valorVista,
                };

                db.FechamentoCaixa.Add(fechamento);
            }
            else
            {
                fechamento.ValorAcrescimo = valorAcrescimo;
                fechamento.ValorCheque = valorCheque;
                fechamento.ValorCrediario = valorCrediario;
                fechamento.ValorDesconto = valorDesconto;
                fechamento.ValorDespesa = valorDespesa;
                fechamento.ValorEntrada = valorEntrada;
                fechamento.ValorJuros = valorJuros;
                fechamento.ValorPrazo = valorPrazo;
                fechamento.ValorVista = valorVista;

                db.FechamentoCaixa.Update(fechamento);
            }

            db.SaveChanges();
        }


        public bool CaixaLancado(string loja, DateTime data)
        {
            return db.FechamentoCaixa
                .Where(e => e.Loja == loja && e.Data == data && e.RowDeleted != "T")
                .Count() > 0;
        }

    }
}