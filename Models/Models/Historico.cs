using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace IdxSistemas.Models
{
    [Table("cad_his")]
    public class Historico : BaseModel
    {
        [Column("COD_HIS")]
        [Required]
        [MaxLength(2)]
        public string Codigo { get; set; }

        [Column("NOM_HIS")]
        [Required]
        [MaxLength(20)]
        public string Nome { get; set; }
    }
}
