using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using IdxSistemas.Models;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.AppRepository.Services;

using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using IdxSistemas.DTO;

namespace IdxSistemas.AppServer.OData.Controllers
{
    public class FechamentoCaixaController : BaseController<FechamentoCaixa>
    {

        private readonly FechamentoCaixaService service;

        public FechamentoCaixaController(DataContext db)
        {
            this.db = db;
            this.service = new FechamentoCaixaService(this.db);
        }


        [HttpGet]
        [ODataRoute("GeraFechamentoCaixa(Loja={loja}, Data={data})")]
        public IActionResult GeraFechamentoCaixa(string loja, DateTime data)
        {
            //try
            //{
                FechamentoCaixa fechamento = this.service.GeraFechamentoCaixa(loja, data);

                if (fechamento == null)
                {
                    ModelState.AddModelError("Erro", "Erro ao gerar fechamento de caixa.");
                    return BadRequest(ModelState);
                }

                List<MovimentoCaixa> depositos =
                    db.MovimentoCaixa
                    .Where(e => e.Loja == loja && e.Data == data && e.RowDeleted != "T")
                    .ToList();
                 
                double? totalDia = fechamento.ValorVista + fechamento.ValorEntrada + fechamento.ValorCrediario;
                double? totalCaixa = 
                    (fechamento.ValorVista + fechamento.ValorEntrada + fechamento.ValorCrediario) - fechamento.ValorDespesa;
                double? totalDepositos = depositos.Sum(e => e.Valor);
                double? total = totalDepositos - 0;
                double? diferencaCaixa = total - totalCaixa;

                FechamentoCaixaDTO dto = new FechamentoCaixaDTO {
                    VendaVista = fechamento.ValorVista,
                    EntradaVendaPrazo = fechamento.ValorEntrada,
                    RecebimentoCrediario = fechamento.ValorCrediario,
                    TotalDia = totalDia,
                    Cheque = fechamento.ValorCheque,
                    Despesas = fechamento.ValorDespesa,
                    TotalCaixa = totalCaixa,
                    Depositos = depositos,
                    TotalDepositos = totalDepositos,
                    TotalRemessaCheques = 0 , // to do
                    Total = total - 0, // to do
                    DiferencaCaixa = diferencaCaixa
                };

                return Ok(dto);
            //}
            //catch (Exception ex)
            //{
                //ModelState.AddModelError("Error", ex.Message);
                //return BadRequest(ModelState);
            //}

        }


        [HttpGet]
        [ODataRoute("FechamentoCaixaLancado(Loja={loja}, Data={data})")]
        public IActionResult FechamentoCaixaLancado(string loja, DateTime data)
        {
            try
            {
                return Ok( this.service.CaixaLancado(loja, data) );
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return BadRequest(ModelState);
            }

        }

    }
}
