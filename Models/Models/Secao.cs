using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdxSistemas.Models
{
    [Table("cad_sec")]
    public class Secao : BaseModel
    {
        [Column("COD_SEC")]
        [Required]
        [MaxLength(2)]
        public String Codigo { get; set; }

        [Column("NOM_SEC")]
        [Required]
        [MaxLength(15)]
        public String Nome { get; set; }
        
    }
}
