using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IdxSistemas.Models
{
    [Table("PERGUNTAS")]
    public class Pergunta : BaseModel
    {
        [MaxLength(5)]
        [Required]
        public string Codigo { get; set; }


        [MaxLength(50)]
        [Required]
        public string Descricao { get; set; }

        // tipo de dado
        [MaxLength(50)]
        [Required]
        public string Tipo { get; set; }

        [MaxLength(50)]
        public string Componente { get; set; }


        [MaxLength(1)]
        [Required]
        public string Obrigatorio { get; set; }

        [MaxLength(1)]
        [Required]
        public string Inativo { get; set; }

        [MaxLength(50)]
        public string Resposta { get; set; }

        [MaxLength(150)]
        public string Lista { get; set; }

        [MaxLength(150)]
        public string ValueHelp { get; set; }


        [MaxLength(2)]
        [Required]
        public string Ordem { get; set; }

        [MaxLength(50)]
        public string Parametro { get; set; }

    }

}
