using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdxSistemas.Models
{
    [Table("arq_mov")]
    public class MovimentoEstoque : BaseModel
    {
        [Column("COD_TRA")]
        [MaxLength(2)]
        [Required]
        /// 01-SALDO INICIAL", "02-ACERTO A MAIOR", "03-ACERTO A MENOR", "04-TRANSFERENCIA
        public string CodigoTransferencia { get; set; }

        [Column("COD_ITE")]
        [MaxLength(14)]
        [Required]
        public string CodigoItem { get; set; }

        [Column("LOC_SAI")]
        [MaxLength(2)]
        public string LocalEstoqueSaida { get; set; } = "00";

        [Column("LOC_ENT")]
        [MaxLength(2)]
        public string LocalEstoqueEntrada { get; set; } = "00";

        [Column("DAT_MOV", TypeName = "date")]
        [Required]
        public DateTime? DataMovimento { get; set; } = DateTime.Now.Date;

        [Column("HIS_MOV")]
        [MaxLength(20)]
        public string Historico { get; set; }

        [Column("NUM_DOC")]
        [MaxLength(10)]
        public string NumeroDocumento { get; set; }

        [Column("QUT_ATU")]
        [Required]
        public int QuantidadeAtual { get; set; } = 1;
    }
}
