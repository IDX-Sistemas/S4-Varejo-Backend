using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdxSistemas.Models
{
    [Table("par_met")]
    public class Parametro : BaseModel
    {
        [Column("DES_PAR")]
        [Required]
        public string Nome { get; set; }

        [Column("VARIAVEL")]
        public string Variavel { get; set; }

        [Column("VAL_PAR")]
        public string Valor { get; set; }
    }
}