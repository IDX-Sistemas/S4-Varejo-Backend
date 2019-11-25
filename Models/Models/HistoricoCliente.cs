using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdxSistemas.Models
{
    [Table("TMP_HISTORICO_CLIENTE")]
    public class HistoricoCliente : BaseModel
    {

        public string Cliente { get; set; }

        public string NumeroDuplicata { get; set; }

        public string Loja { get; set; }
        
        public string NumeroDocumento { get; set; }

        public string TipoVenda { get; set; }

        public DateTime? DataEmissao { get; set; }

        public DateTime DataVencimento { get; set; }

        public DateTime? DataPagamento { get; set; }

        public double? ValorDuplicata { get; set; }

        public double? ValorPago { get; set; }
      
    }
}