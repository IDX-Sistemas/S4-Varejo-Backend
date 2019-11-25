using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdxSistemas.Models
{
    [Table("tmp_con_rec")]
    public class ContaReceberTemp : BaseModel
    {
        [Column("COD_CLI")]
        [Required]
        [MaxLength(5)]
        public string Cliente { get; set; }

        [ForeignKey("Cliente")]
        public virtual Cliente Clientes { get; set; }

        [Column("LOC_PAG")]
        [Required]
        [MaxLength(2)]
        public string Loja { get; set; }
        
        [Column("NUM_DOC")]
        [MaxLength(6)]
        public string NumeroDocumento { get; set; }

        [Column("COD_VEN")]
        [MaxLength(2)]
        public string Vendedor { get; set; }

        [Column("NUM_DUP")]
        [MaxLength(8)]
        public string NumeroDuplicata { get; set; }

        [Column("VAL_DUP")]
        public double? ValorDuplicata { get; set; } = 0.00;

        [Column("NUM_FAT")]
        [MaxLength(6)]
        public string NumeroFatura { get; set; }

        [Column("VAL_FAT")]
        public double? ValorFatura { get; set; } = 0.00;

        [Column("VAL_JUR")]
        public double? Juros { get; set; } = 0.00;

        [Column("VAL_DES")]
        public double? Desconto { get; set; } = 0.00;

        [Column("DAT_EMI", TypeName = "DATE")]
        public DateTime? DataEmissao { get; set; } = DateTime.Now.Date;

        [Column("DAT_VEN",TypeName = "DATE")]
        public DateTime DataVencimento { get; set; }

        [Column("NUM__CI")]
        [MaxLength(6)]
        public string NumeroCI { get; set; }

        [Column("VAL_TOT")]
        public double? ValorTotal { get; set; } = 0.00;

        [Column("FLA_PAG")]
        public Int16? FlagPgto { get; set; } = 0;

        [Column("TIP_DUP")]
        [MaxLength(1)]
        public string TipoVenda { get; set; }

        [Column("FLA_ENT")]
        [MaxLength(1)]
        public string FlagEntrada { get; set; }

    }

}