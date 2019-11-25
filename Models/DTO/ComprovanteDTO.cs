using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdxSistemas.DTO
{
    /*passar uma lista como parâmetro, mesmo se houver somente
      um recebimento */
    public class ComprovanteDTO
    {
        public string CodigoCliente { get; set; }

        public string NomeCliente { get; set; }

        public string CpfCliente { get; set; }

        public string Endereco { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public string Duplicata { get; set; }

        public string Loja { get; set; }

        public DateTime DataPagamento { get; set; }

        //con_rec
        public DateTime DataEmissao { get; set; }

        //con_rec
        public DateTime DataVencimento { get; set; }

        //valor restante após calcular os recebimentos na fil_rec
        public double ValorReceber { get; set; }

        public double ValorPago { get; set; }
    }
}