using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdxSistemas.Models
{
    [Table("age_loj")]
    public class Agenda : BaseModel
    {

        [Column("NOM_ITE")]
        [MaxLength(40)]
        public string Nome { get; set; }

        [Column("END_ITE")]
        [MaxLength(40)]
        public string Endereco { get; set; }

        [Column("BAI_ITE")]
        [MaxLength(15)]
        public string Bairro { get; set; }

        [Column("CID_ITE")]
        [MaxLength(15)]
        public string Cidade { get; set; }

        [Column("TEL_ITE")]
        [MaxLength(15)]
        public string Telefone { get; set; }

        [Column("EMA_ITE")]
        [MaxLength(40)]
        public string Email { get; set; }



    }
}
