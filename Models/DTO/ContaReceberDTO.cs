using System;

namespace IdxSistemas.DTO
{
    public class ContaReceberDTO
    {
        public long RowId {get; set; }

        public string Cliente { get; set; }

        public string Loja { get; set; }
        
        public string NumeroDocumento { get; set; }

        public string Vendedor { get; set; }

        public string NumeroDuplicata { get; set; }

        public double? ValorDuplicata { get; set; }

        public string NumeroFatura { get; set; }

        
        public double? Juros { get; set; } 

        public double? Desconto { get; set; } 

        public DateTime? DataEmissao { get; set; } = DateTime.Now.Date;

        public DateTime DataVencimento { get; set; }


        public double? ValorTotal { get; set; }

        public Int16? FlagPgto { get; set; } 

        public string TipoVenda { get; set; }


        public double? Saldo { get; set; }

        public double? ValorReceber {get; set;}

        public int? Atraso {get ; set; }
    }
}