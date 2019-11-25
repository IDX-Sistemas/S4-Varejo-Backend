using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdxSistemas.Models
{
    [Table("cad_ord")]
    public class Tipo : BaseModel
    {
        [Column("COD_ORD")]
        [Required]
        [MaxLength(3)]
        public string Codigo { get; set; }

        [Column("NOM_ORD")]
        [MaxLength(15)]
        public string Nome { get; set; }
    }
}
