using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdxSistemas.Models
{
    [Table("cad_loc")]
    public class Loja : BaseModel
    {
        [Column("COD_LOC")]
        [Required]
        [MaxLength(2)]
        public string Codigo { get; set; }

        [Column("NOM_LOC")]
        [Required]
        [MaxLength(15)]
        public string Nome { get; set; }

        [Column("MOV_EST")]
        [MaxLength(1)]
        public string MovimentaEstoque { get; set; } = "N";
    }
}
