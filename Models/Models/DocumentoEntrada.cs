using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdxSistemas.Models
{
    [Table("cad_com")]
    public class DocumentoEntrada : BaseModel
    {

        public DocumentoEntrada()
        {
            this.DocumentoEntradaItems = new HashSet<DocumentoEntradaItem>();
        }
       
        [Column("NUM_NOT")]
        [Required]
        [MaxLength(8)]
        public string Numero { get; set; } = "";

        [Column("NUM_DUP")]
        [MaxLength(6)]
        public string NumeroDuplicata { get; set; } = "";

        [Column("CLA_FIS")]
        [MaxLength(3)]
        public string ClassificacaoFiscal { get; set; } = "";

        [Column("NUM_SER")]
        [MaxLength(3)]
        public string Serie { get; set; } = "";

        [Column("NAT_OPE")]
        [MaxLength(3)]
        public string NaturezaOperacao { get; set; } = "";

        [Column("DAT_COM", TypeName = "date")]
        public DateTime? DataEmissao { get; set; } = DateTime.Now;

        [Column("DAT_ENT", TypeName = "date")]
        public DateTime? DataRecebimento { get; set; }

        [Column("BAS_CAL")]
        public double? BaseCalculo { get; set; } = 0;

        [Column("VAL_ICM")]
        public double? ValorICMS { get; set; } = 0;

        [Column("VAL_IPI")]
        public double? ValorIPI { get; set; } = 0;

        [Column("VAL_TOT")]
        public double? ValorTotal { get; set; } = 0;

        [Column("COD_LOC")]
        [Required]
        public string Loja { get; set; }

        [Column("COD_FOR")]
        [Required]
        public string Fornecedor { get; set; } 

        [ForeignKey("Fornecedor")]
        [InverseProperty("NotaFiscal")]
        public virtual Fornecedor Fornecedores {get; set; }

        [Column("CON_PAG")]
        [MaxLength(20)]
        public string CondicaoPagamentoInfo { get; set; } = "";

        [Column("STA_COM")]
        public string Classificacao { get; set; } = "N";

        [Column("CONDICAO_PAGAMENTO")]
        public string Condicao { get; set; }

        [ForeignKey("Condicao")]
        public CondicaoPagamento CondicaoPagamento { get; set; }

        public ICollection<DocumentoEntradaItem> DocumentoEntradaItems { get; set; }

    }
}