using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IdxSistemas.Models
{
    [Table("arq_vxx")]
    public class VendaSecao : BaseModel
    {
        [Column("COD_LOC")]
        public string Loja{ get; set; }

        [Column("COD_SEC")]
        public string Secao { get; set; }

        [Column("VAL_VIS")]
        public double? ValorVista { get; set; }

        [Column("VAL_PRA")]
        public double? ValorPrazo { get; set; }

        [Column("VAL_ENT")]
        public double? ValorEntrada { get; set; }

    }
}
