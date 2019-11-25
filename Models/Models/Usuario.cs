using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdxSistemas.Models
{
    [Table("cad_ace")]
    public class Usuario : BaseModel
    {
        [Column("NOM_USR")]
        [Required]
        [MaxLength(10)]
        public string Nome { get; set; }

        [Column("SEN_USR")]
        [Required]
        [MaxLength(10)]
        public string Senha { get; set; }
    }
}
