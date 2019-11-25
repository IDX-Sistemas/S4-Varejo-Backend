using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdxSistemas.Models
{
    [Table("cad_opr")]
    public class Operadora : BaseModel
    {
        [Column("COD_CLI")]
        public string Admin { get; set; }
        
        [ForeignKey("Admin")]
        [InverseProperty("PlanoCartao")]
        public virtual Cliente Administrador { get; set; }

        [Column("DES_OPR")]
        [MaxLength(50)]
        public string Descricao { get; set; }

        [Column("TAX_OPR")]
        public double? Taxa { get; set; } = 0.00;

        [Column("PAR_INI")]
        public int ParcelaInicial { get; set; } = 0;

        [Column("PAR_FIN")]
        public int ParcelaFinal { get; set; } = 0;

        [Column("FLG_STA")]
        [MaxLength(1)]
        public string FlagStatus { get; set; }

        [Column("TIP_VEN")]
        public int TipoVencimento { get; set; }
    }
}
