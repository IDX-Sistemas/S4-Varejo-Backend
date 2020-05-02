using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IdxSistemas.Models
{
    public class UsuarioFuncao 
    {
        [Key]
        [Required, MaxLength(15)]

        public string FuncaoId { get; set; }

        [Required]
        public string Descricao { get; set; }
    }
}
