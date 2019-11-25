using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdxSistemas.Models
{
    [Table("cad_che")]
    public class Cheque : BaseModel
    {
        [Column("COD_LOC")]
        [MaxLength(2)]
        [Required]
        public string Loja { get; set; }

        [ForeignKey("Loja")]
        public virtual Loja Lojas { get; set; }

        [Column("DAT_LAN", TypeName = "date")]
        public DateTime? Data { get; set; }

        [Column("BAN_CHE")]
        [MaxLength(3)]
        [Required]
        public string Banco { get; set; }


        [Column("AGE_CHE")]
        [MaxLength(3)]
        [Required]
        public string Agencia { get; set; }

        [Column("CON_CHE")]
        [MaxLength(12)]
        [Required]
        public string Conta { get; set; }

        [Column("NUM_CHE")]
        [MaxLength(7)]
        [Required]
        public string Numero { get; set; }

        [Column("VAL_CHE")]
        public double? Valor { get; set; }

        [Column("NUM_ETP")]
        [MaxLength(1)]
        [Required]
        public string Etapa { get; set; }

        [Column("DAT_VEN", TypeName = "date")]
        public DateTime? Vencimento { get; set; }

        [Column("COD_CLI")]
        [MaxLength(5)]
        public string Cliente { get; set; }

        [ForeignKey("Cliente")]
        public virtual Cliente Clientes { get; set; }
    }

}