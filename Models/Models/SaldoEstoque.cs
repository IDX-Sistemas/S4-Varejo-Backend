using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdxSistemas.Models
{
    [Table("loc_ite")]
    public class SaldoEstoque : BaseModel
    {
        [Column("COD_ITE")]
        [MaxLength(14)]
        [Required]
        public string Codigo { get; set; }

        [Column("COD_LOC")]
        [MaxLength(2)]
        [Required]
        public string Loja { get; set; }

        [ForeignKey("Loja")]
        public virtual Loja Lojas {get; set;}

        [Column("EST_ATU")]
        public int? SaldoAtual { get; set; }
    }
}
