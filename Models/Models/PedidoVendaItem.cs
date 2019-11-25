using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdxSistemas.Models
{
    [Table("ite_not")]
    public class PedidoVendaItem : BaseModel
    {
        [Column("NUM_VEN")]
        [Required]
        public string NumeroVenda { get; set; }

        [Column("COD_LOC")]
        [Required]
        public string Loja { get; set; }

        [Column("COD_ITE")]
        [Required]
        public string Codigo { get; set; }

        [ForeignKey("Codigo")]
        public virtual Produto Produtos {get ; set;}

        [Column("VAL_UNI")]
        public double? ValorUnitario { get; set; }

        [Column("VAL_DES")]
        public double? ValorDesconto { get; set; }

        [Column("VAL_ACR")]
        public double? ValorAcrescimo { get; set; }

        [Column("QUT_ITE")]
        public int? Quantidade { get; set; }

        [Column("TRO_CA")]
        public string Troca { get; set; }

        [Column("COD_VEN")]
        public string Vendedor { get; set; }

        [ForeignKey("Vendedor")]
        public virtual Vendedor Vendedores { get; set; }

        [Column("COD_SEC")]
        public string Secao { get; set; }

        public virtual PedidoVenda PedidoVenda { get; set; }

        [Column("NUM_CTL")]
        [MaxLength(6)]
        public string NumeroPreVenda { get; set; }
    }
}
