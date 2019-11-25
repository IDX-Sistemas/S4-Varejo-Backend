using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdxSistemas.Models
{
    [Table("con_cor")]
    public class ContaCorrente : BaseModel
    {
        [Column("COD_CON")]
        [Required]
        [MaxLength(4)]
        public string CodigoConta {get; set; }

        [ForeignKey("CodigoConta")]
        public virtual ContaBancaria ContaBancaria {get; set;}

        [Column("DEB_CRE")]
        [Required]
        [MaxLength(1)]
        /// C-CREDITO, D-DEBITO, R-CARTAO, B-TARIFAS
        public string DebitoCredito {get; set; }

        [Column("NUM_CHE")]
        [MaxLength(6)]
        public string Cheque {get; set; }

        [Column("NOM_HIS")]
        [MaxLength(29)]
        public string Historico {get; set; }

        [Column("DAT_LAN", TypeName = "date")]
        [Required]
        public DateTime Data {get; set; } = DateTime.Now;

        [Column("NUM_DUP")]
        [MaxLength(8)]
        // numero da duplicata paga
        public string NumeroDuplicata {get; set; }  
            
        [Column("COD_FOR")]
        [MaxLength(4)]
        // codigo do fornecedor da duplicata paga
        public string CodigoFornecedor {get; set; } 

        [ForeignKey("CodigoFornecedor")]
        public virtual Fornecedor Fornecedor {get; set; } 

        [Column("VAL_LAN")]
        public double? Valor { get; set; } = 0;

    }
}