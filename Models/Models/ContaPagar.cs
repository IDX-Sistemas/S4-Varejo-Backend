using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace IdxSistemas.Models
{
    [Table("con_pag")]
    public class ContaPagar : BaseModel
    {
        [Column("NUM_DOC")]
        [MaxLength(6)]
        public string NumeroDocumento { get; set; }

        [Column("COD_LOC")]
        [MaxLength(2)]
        public string Loja { get; set; }

        [Column("NUM_CHE")]
        [MaxLength(6)]
        public string NumeroCheque { get; set; }

        [Column("COD_FOR")]
        [MaxLength(4)]
        public string Fornecedor { get; set; }

        [ForeignKey("Fornecedor")]
        public virtual Fornecedor Fornecedores { get; set; }
        
        [Column("NUM_NOT")]
        [MaxLength(8)]
        public string NotaFiscal { get; set; }

        [Column("DAT_EMI", TypeName = "date")]
        public DateTime? DataEmissao { get; set; } = DateTime.Now.Date;

        [Column("CLA_FIS")]
        [MaxLength(1)]
        public string Classificacao { get; set; }

        [Column("NUM_DUP")]
        [MaxLength(8)]
        public string Duplicata { get; set; }

        [Column("VAL_DUP")]
        public double? ValorDuplicata { get; set; } = 0.00;

        [Column("HIS_DUP")]
        [MaxLength(30)]
        public string Historico { get; set; }

        [Column("VAL_JUR")]
        public double? Juros { get; set; } = 0.00;

        [Column("VAL_DES")]
        public double? Desconto { get; set; } = 0.00;

        [Column("VAL_PAG")]
        public double? ValorPago { get; set; } = 0.00;

        [Column("DAT_VEN", TypeName = "date")]
        public DateTime? DataVencimento { get; set; } = DateTime.Now.Date;

        [Column("DAT_REC", TypeName = "date")]
        public DateTime? DataRecebimento { get; set; } = DateTime.Now.Date;

        [Column("DAT_PAG", TypeName = "date")]
        public DateTime? DataPagamento { get; set; }

        [Column("COD_CON")]
        public string CodigoConta { get; set; }
    }
}
