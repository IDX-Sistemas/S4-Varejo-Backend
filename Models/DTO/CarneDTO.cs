using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdxSistemas.DTO
{
    public class CarneDTO
    {
        public string NumeroCi { get; set; }

        public string CodigoCliente { get; set; }

        public string NomeCliente { get; set; }

        public string CpfCliente { get; set; }

        public string Endereco { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public DateTime Emissao { get; set; }

        public int Parcelas { get; set; }

        public double ValorTotal { get; set; }

        public List<CarneVencimentoDTO> Vencimentos { get; set; }
    }

    public class CarneVencimentoDTO
    {
        public DateTime Vencimento { get; set; }

        public double Valor { get; set; }
    }
}