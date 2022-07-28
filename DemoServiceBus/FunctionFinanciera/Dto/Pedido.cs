using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionFinanciera.Dto
{
    public class Pedido
    {
        public int IdPedido { get; set; }

        public int IdCliente { get; set; }

        public int IdProducto { get; set; }

        public int Cantidad { get; set; }
    }
}
