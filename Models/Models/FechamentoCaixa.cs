using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IdxSistemas.Models
{
    [Table("arq_cai")]
    public class FechamentoCaixa : BaseModel
    {
        [Column("LOJ")]
        public string Loja { get; set; }

        [Column("DAT")]
        public DateTime Data { get; set; }

        [Column("VIS")]
        public double? ValorVista { get; set; }

        [Column("ENT")]
        public double? ValorEntrada { get; set; }

        [Column("CRE")]
        public double? ValorCrediario { get; set; }

        [Column("PRA")]
        public double? ValorPrazo { get; set; }

        [Column("CHE")]
        public double? ValorCheque { get; set; }

        [Column("ACR")]
        public double? ValorAcrescimo { get; set; }

        [Column("DSC")]
        public double? ValorDesconto { get; set; }

        [Column("JUR")]
        public double? ValorJuros { get; set; }

        [Column("DSP")]
        public double? ValorDespesa { get; set; }
    }
}
