using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdxSistemas.DTO
{
    public class ExtratoCarneDTO
    {
        public string CodigoCliente { get; set; }

        public string NomeCliente { get; set; }

        public string CpfCliente { get; set; }

        public string Endereco { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public List<ExtratoCarneVencimentosDTO> Vencimentos;
    }

    public class ExtratoCarneVencimentosDTO
    {
        public string Duplicata { get; set; }

        public DateTime Vencimento { get; set; }

        public double Valor { get; set; }
    }
}
