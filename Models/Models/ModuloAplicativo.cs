using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IdxSistemas.Models
{
    public class ModuloAplicativo : BaseModel
    {
        [MaxLength(50), Required]
        public string ModuloCodigo { get; set; }

        [ForeignKey("ModuloCodigo")]
        public Modulo Modulo { get; set; }

        [Required, MaxLength(200)]
        public string AplicativoCodigo { get; set; }

        [ForeignKey("AplicativoCodigo")]
        public Aplicativo Aplicativo { get; set; }
    }

}
