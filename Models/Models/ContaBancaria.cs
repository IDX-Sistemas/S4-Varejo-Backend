using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdxSistemas.Models
{
    [Table("cad_con")]
    public class ContaBancaria : BaseModel
    {
        [Column("COD_CON")]
        [Required(ErrorMessage = "Informe o código!")]
        [MaxLength(4)]
        public string Codigo { get; set; }

        [Column("NOM_CON")]
        [Required(ErrorMessage = "Informe a conta!")]
        [MaxLength(15)]
        public string Conta { get; set; }

        [Column("NOM_BAN")]
        [MaxLength(15)]
        public string NomeBanco { get; set; }

        [Column("DES_CON")]
        [MaxLength(20)]
        public string Descricao { get; set; }

        [Column("VAL_SAL")]
        public double? Saldo { get; set; } = 0;

        [Column("ULT_LAN", TypeName = "date")]
        public DateTime? UltimoLancamento { get; set; } = DateTime.Now.Date;

        [Column("COD_LOC")]
        [MaxLength(2)]
        public string Loja { get; set; }
    }
}
