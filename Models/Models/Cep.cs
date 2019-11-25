using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdxSistemas.Models
{
    [Table("cep_rua")]
    public class Cep : BaseModel
    {
        [Column("CEP")]
        [Required]
        [MaxLength(8)]
        public string NumeroCep { get; set; }

        [Column("RUA")]
        [MaxLength(55)]
        public string Rua { get; set; }

        [Column("BAI")]
        [MaxLength(18)]
        public string Bairro { get; set; }

        [Column("CID")]
        [MaxLength(20)]
        public string Cidade { get; set; }

        [Column("EST")]
        [MaxLength(2)]
        public string Estado { get; set; }
    }
}
