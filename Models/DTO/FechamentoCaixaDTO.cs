using IdxSistemas.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdxSistemas.DTO
{
    public class FechamentoCaixaDTO
    {
        public double? VendaVista { get; set; }

        public double? EntradaVendaPrazo { get; set; }

        public double? RecebimentoCrediario { get; set; }

        public double? TotalDia { get; set; }

        public double? Cheque { get; set; }

        public double? Despesas { get; set; }

        public double? TotalCaixa { get; set; }

        public List<MovimentoCaixa> Depositos { get; set; }

        public double? TotalDepositos { get; set; }

        public double? TotalRemessaCheques { get; set; }

        public double? Total { get; set; }

        public double? DiferencaCaixa { get; set; }
    }
}
