using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdxSistemas.Models
{
    [Table("fil_rec")]
    public class RecebimentoConta : BaseModel
    {
        [Column("NUM_DUP")]
        public string NumeroDuplicata { get; set; }

        [Column("DAT_PAG", TypeName = "date")]
        public DateTime? DataPagamento { get; set; } = DateTime.Now;

        [Column("VAL_PAG")]
        public double? ValorPago { get; set; } = 0.00;

        [Column("NUM_DOC")]
        public string NumeroDocumento { get; set; }

        [Column("COD_LOC")]
        public string Loja { get; set; }

        [Column("VAL_JUR")]
        public double? ValorJuros { get; set; } = 0.00;

        [Column("VAL_DES")]
        public double? ValorDesconto { get; set; } = 0.00;


        [ForeignKey("NumeroDuplicata, Loja")]
        public virtual ContaReceber ContaReceber { get; set; }

    }
}
