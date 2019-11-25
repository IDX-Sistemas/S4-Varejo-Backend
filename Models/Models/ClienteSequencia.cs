using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  IdxSistemas.Models
{
    [Table("cli_cod")]
    public class ClienteCodigo : BaseModel
    {
        [Column("COD_CLI")]
        [Required]
        public string Codigo { get; set; }

    }
}