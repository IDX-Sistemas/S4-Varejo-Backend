using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdxSistemas.Models
{
    [Table("cad_ven")]
    public class Vendedor : BaseModel
    {
        [Column("COD_VEN")]
        [Required]
        [MaxLength(2)]
       public string Codigo { get; set; }

        [Column("NOM_VEN")]
        [Required]
        [MaxLength(20)]
        public string Nome { get; set; }

        [Column("COD_LOC")]
        [Required]
        [MaxLength(2)]
        public string Loja { get; set; }

        [Column("DAT_CAD", TypeName = "date")]
        public DateTime? DataCadastro { get; set; } = DateTime.Now.Date;

        [Column("TAX_COM")]
        public double? Comissao { get; set; } = 0;

        [Column("VAL_ADI")]
        public double? Adiantamento { get; set; } = 0;

        [Column("VAL_COT")]
        public double? CotaVenda { get; set; } = 0;

        [Column("VAL_FIX")]
        public double? ValorFixo { get; set; } = 0;

        [Column("PRAZO")]
        public double? ValorPrazo { get; set; } = 0;

        [Column("VISTA")]
        public double? ValorVista { get; set; } = 0;

        [Column("COD_SE1")]
        [MaxLength(2)]
        public string Secao1 { get; set; }

        [Column("COD_SE2")]
        [MaxLength(2)]
        public string Secao2 { get; set; }

        [Column("COD_SE3")]
        [MaxLength(2)]
        public string Secao3 { get; set; }

        [Column("COD_SE4")]
        [MaxLength(4)]
        public string Secao4 { get; set; }

        [Column("COD_SE5")]
        [MaxLength(2)]
        public string Secao5 { get; set; }

        [Column("COD_SE6")]
        [MaxLength(2)]
        public string Secao6 { get; set; }
    }
}