using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IdxSistemas.Models
{
    public class Modulo
    {
        [Key]
        [MaxLength(50), Required]
        public string Codigo { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required, MaxLength(15)]
        public string UsuarioFuncaoId { get; set; }

        [ForeignKey("UsuarioFuncaoId")]
        public UsuarioFuncao UsuarioFuncao { get; set; }

        [MaxLength(3)]
        public string Sequencia { get; set; }

    }

}
