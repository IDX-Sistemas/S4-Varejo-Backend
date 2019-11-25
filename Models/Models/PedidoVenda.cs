using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdxSistemas.Models
{
    [Table("cad_not")]
    public class PedidoVenda : BaseModel
    {
        public PedidoVenda()
        {
            this.PedidoVendaItems  = new HashSet<PedidoVendaItem>();
        }

        [Column("NUM_VEN")]
        [MaxLength(6)]
        [Required]
        public string Numero { get; set; }

        [Column("COD_CLI")]
        [MaxLength(5)]
        public string Cliente { get; set; }

        [ForeignKey("Cliente")]
        public virtual Cliente Clientes { get; set; }

        [Column("COD_LOC")]
        [MaxLength(2)]
        public string Loja { get; set; }

        [Column("DAT_VEN", TypeName = "date")]
        public DateTime? Data { get; set; }

        [Column("TIP_VEN")]
        public string TipoVenda { get; set; }

        [Column("NUM_PAR")]
        public int? Parcelas { get; set; }

        [Column("FLA_ENT")]
        public string FlagEntrada { get; set; }

        [Column("VAL_VEN")]
        public double? ValorVenda { get; set; }

        [Column("VAL_DES")]
        public double? ValorDesconto { get; set; }

        [Column("VAL_ACR")]
        public double? ValorAcrescimo { get; set; }

        [Column("NUM_DOC")]
        public string NumeroDocumento { get; set; }

        [Column("COD_CAR")]
        public long? OperadoraId { get; set; }

        public virtual Operadora Operadora { get; set; }

        [Column("VAL_ENT")]
        public double? ValorEntrada { get; set; }

        [Column("NOT_FAT")]
        [MaxLength(1)]
        public string Faturado { get; set; }

        public virtual ICollection<PedidoVendaItem> PedidoVendaItems {get ; set; }

        [NotMapped]
        public double? Total
        {
            get
            {
                return ((ValorVenda + ValorAcrescimo) - ValorDesconto ) ?? 0d;
            }
        }

    }

  
}
