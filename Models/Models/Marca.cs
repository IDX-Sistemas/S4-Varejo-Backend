using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdxSistemas.Models
{
    [Table("cad_mar")]
    public class Marca : BaseModel
    {
        [Column("COD_MAR")]
        [Required]
        [MaxLength(3)]
        public string Codigo { get; set; }

        [Column("NOM_MAR")]
        [MaxLength(15)]
        public string Nome { get; set; }
    }
}
