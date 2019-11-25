using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdxSistemas.Models
{
    [Table("mov_cai")]
    public class MovimentoCaixa : BaseModel
    {
        [Column("COD_LOC")]
        public string Loja { get; set; }

        [ForeignKey("Loja")]
        public virtual Loja Lojas { get; set; }

        [Column("DAT_LAN", TypeName = "date")]
        public DateTime Data { get; set; }

        [Column("VAL_LAN")]
        public double Valor { get; set; }

        [Column("NOM_HIS")]
        public string Historico { get; set; }
    }
}