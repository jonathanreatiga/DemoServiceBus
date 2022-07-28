namespace ApiTienda.Entities
{
    public class Pedido
    {
        public int IdPedido { get; set; }

        public int IdCliente { get; set; }

        public int IdProducto { get; set; }

        public int Cantidad { get; set; }
    }
}
