using System.ComponentModel.DataAnnotations.Schema;

namespace IdxSistemas.Models
{
    [Table("ite_ctl")]
    public class PreVendaItem : BaseModel
    {
        [Column("NUM_VEN")]
        public string Numero { get; set; }

        [ForeignKey("Numero")]
        public virtual PreVenda PreVenda { get; set; }

        [Column("COD_LOC")]
        public string Loja { get; set; }

        [Column("COD_ITE")]
        public string Codigo { get; set; }
        
        [Column("VAL_UNI")]
        public double? ValorUnitario { get; set; } = 0.00;

        [Column("QUT_ITE")]
        public int? Quantidade { get; set; } = 1;

        [Column("VAL_DES")]
        public double? Desconto { get; set; } = 0.00;

        [Column("VAL_ACR")]
        public double? Acrescimo { get; set; } = 0.00;
    }
}
