using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdxSistemas.Models
{
    [Table("ite_com")]
    public class DocumentoEntradaItem : BaseModel
    {
        [Column("NUM_NOT")]
        [Required]
        [MaxLength(8)]
        public string DocumentoEntradaNumero { get; set; } = "";

        [Column("COD_ITE")]
        [MaxLength(14)]
        [Required]
        public string Codigo { get; set; } = "";

        [ForeignKey("Codigo")]
        public virtual Produto Produto { get; set; }


        [Column("VAL_UNI")]
        public double ValorUnitario { get; set; } = 0.00;

        [Column("QUT_ITE")]
        public int Quantidade { get; set; } = 0;

        [NotMapped]
        public double ValorTotal {
            get
            {
                return ValorUnitario * Quantidade;
            }
        }

        [Column("COD_FOR")]
        [Required]
        public string Fornecedor { get; set; } = "";

        [Column("COD_LOC")]
        [Required]
        public string Loja { get; set; } = "";

        [Column("DAT_ENT", TypeName = "date")]
        [Required]
        public DateTime DataEntrada { get; set; } = DateTime.Now;


        [Column("CLA")]
        public string Classificacao{ get; set; } = "";

        [Column("ENT_ANT")]
        [MaxLength(1)]
        public string EntradaAntecipada { get; set; } = "N";

        public virtual DocumentoEntrada DocumentoEntrada { get; set; }
    }
}
