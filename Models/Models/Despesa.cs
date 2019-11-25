using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdxSistemas.Models
{
    [Table("cad_des")]
    public class Despesa : BaseModel
    {
        [Column("COD_DES")]
        [Required(ErrorMessage="Informe o código!")]
        [MaxLength(2)]
        public string Codigo { get; set; }

        [Column("NOM_DES")]
        [Required(ErrorMessage = "Informe a descrição!")]
        [MaxLength(15)]
        public string Descricao { get; set; }
    }
}
