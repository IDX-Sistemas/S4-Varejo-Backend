using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IdxSistemas.Models
{
    [Table("RELATORIOS")]
    public  class Relatorios : BaseModel
    {
        public string Codigo { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public string Modulo { get; set; }

        public string Link { get; set; }

        public string Executar { get; set; }
    }
}
