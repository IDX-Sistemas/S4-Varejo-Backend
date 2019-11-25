using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace IdxSistemas.Models
{
    [Table("cad_not")]
    public class Venda : BaseModel
    {
        [Column("NUM_VEN")]
        [MaxLength(6)]
        [Required]
        public string Numero { get; set; }

        [Column("COD_CLI")]
        [MaxLength(5)]
        public string Cliente { get; set; }

        [Column("COD_LOC")]
        [MaxLength(2)]
        public string Loja { get; set; }

        [Column("COD_VE1")]
        [MaxLength(2)]
        public string Vendedor { get; set; }

        [Column("DAT_VEN", TypeName = "date")]
        public DateTime? Data { get; set; } = DateTime.Now.Date;

        [Column("VAL_VEN")]
        public double ValorVenda { get; set; } = 0.00;

        [Column("NUM_DOC")]
        public string Documento { get; set; }

        [Column("VAL_DES")]
        public double? ValorDesconto { get; set; } = 0.00;

        [Column("VAL_ACR")]
        public double? ValorAcrescimo { get; set; } = 0.00;

        [Column("NUM_DOC")]
        public string NumeroDocumento { get; set; }

        [Column("FLA_ENT")]
        public string  FlagEntrada { get; set; }
    }
}
