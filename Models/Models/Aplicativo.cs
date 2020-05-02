using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IdxSistemas.Models
{
    public class Aplicativo 
    {
        [Key]
        [Required, MaxLength(200)]
        public string Codigo { get; set; }

        [Required]
        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public string Icone { get; set; }

        [Required]
        public string TargetURL { get; set; }

    }
}
