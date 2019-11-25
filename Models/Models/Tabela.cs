using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdxSistemas.Models
{
    [Table("cad_tab")]
    public class Tabela : BaseModel
    {
        [Column("COD_TAB")]
        [Required]
        [MaxLength(4)]
        public string Codigo { get; set; }

        [Column("NOM_TAB")]
        [MaxLength(30)]
        public string Nome { get; set; }

        [Column("DAT_TAB", TypeName = "date")]
        public DateTime? Data { get; set; } = DateTime.Now.Date;

        [Column("ULT_ALT", TypeName = "date")]
        public DateTime? UltimaAlteracao { get; set; }

        [Column("MAR_GEM")]
        public double? Margem { get; set; } = 0;
    }
}
