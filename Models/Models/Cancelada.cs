using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdxSistemas.Models
{
    [Table("cad_can")]
    public class Cancelada : BaseModel
    {
        [Column("DAT_LAN", TypeName = "date")]
        public DateTime Data { get; set; }

        [Column("NOM_HIS")]
        [MaxLength(25)]
        public string Historico { get; set; }

        [Column("PEN_CAN")]
        [MaxLength(1)]
        public string Status { get; set; }

        [Column("NUM_DOC")]
        [MaxLength(6)]
        public string Numero { get; set; }

        [Column("COD_LOC")]
        [MaxLength(2)]
        public string Loja { get; set; }

        [ForeignKey("Loja")]
        public virtual Loja Lojas { get; set; }
    }
}