using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionFinanciera.Entities
{
    public class Ganancia
    {
        public int IdGanancia { get; set; }

        public int IdProducto { get; set; }

        public int Unidades { get; set; }

        public double Monto { get; set; }
    }
}
