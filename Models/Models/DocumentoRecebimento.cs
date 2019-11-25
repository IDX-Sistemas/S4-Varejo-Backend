using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdxSistemas.Models
{
    [Table("pag_doc")]
    public class DocumentoRecebimento : BaseModel
    {
        [Column("DAT_PAG", TypeName = "date")]
        public DateTime? Data { get; set; }

        [Column("NUM_DOC")]
        public string Numero { get; set; }

        [Column("LOC_PAG")]
        public string Loja { get; set; }
    }
}