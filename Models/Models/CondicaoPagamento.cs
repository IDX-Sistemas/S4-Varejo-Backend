using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IdxSistemas.Models
{
    [Table("CONDICAO_PAGAMENTO")]
    public class CondicaoPagamento : BaseModel
    {
        [MaxLength(length:4, ErrorMessage ="4 POSICOES")]
        [Required]
        public string Codigo { get; set; } = "";

        [MaxLength(length: 100, ErrorMessage = "100 POSICOES")]
        [Required]
        public string Descricao { get; set; } = "";

        public int Intervalo { get; set; } = 0;

        public int Parcelas { get; set; } = 0;

        [Required]
        public string ComEntrada { get; set; } = CondicaoPagamentoComEntrada.NAO;
    }

    public static class CondicaoPagamentoComEntrada
    {
        public  const string NAO = "N";
        public  const string SIM = "S";
    }
}
