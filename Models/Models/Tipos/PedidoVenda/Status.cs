using System;
using System.Collections.Generic;
using System.Text;

namespace IdxSistemas.Models.Tipos.PedidoVenda
{
    public class Status
    {
        public const int OK = 1;

        public const int NAO_ENCONTRADO = 4;

        public const int PEDIDO_RECEBIDO = 2;

        public const int PEDIDO_FATURADO = 3;
    }
}
