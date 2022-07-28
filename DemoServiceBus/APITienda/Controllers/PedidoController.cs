using ApiTienda.Context;
using ApiTienda.Dto;
using ApiTienda.Entities;
using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiTienda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly ApiContext _context;
        readonly IConfiguration _configuration;

        public PedidoController(ApiContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("GetAll")]
        public async Task<object> Index()
        {
            DbSet<Pedido> Pedidos = _context.Pedido;
            DbSet<Producto> productos = _context.Producto;

            var query =           
                from producto in productos
                join pedido in Pedidos
                on producto.IdProducto equals pedido.IdProducto
                select new PedidosDto
                {
                    Nombre = producto.Nombre,
                    Descripcion = producto.Descripcion,
                    Cantidad = pedido.Cantidad
                };
            List<PedidosDto> pedidoDtos = query.ToList();
            return pedidoDtos;
        }

        [HttpPost("SetProducto")]
        public async Task<object> SetPedido(OrdenDto ordenDto)
        {
            Pedido pedido = new Pedido();
            pedido.IdCliente = 1;
            pedido.IdProducto = ordenDto.IdProducto;
            pedido.Cantidad = ordenDto.Cantidad;
            await _context.Pedido.AddAsync(pedido);
            _context.SaveChanges();

            // Eviar a Service Bus
            var queueName = _configuration["ServiceBus:queueName"];
            var connectionService = _configuration["ServiceBus:connectionString"];

            await using var client = new ServiceBusClient(connectionService);
            ServiceBusSender sender = client.CreateSender(queueName);

            string mensajePedido= JsonSerializer.Serialize(pedido);
            ServiceBusMessage message = new ServiceBusMessage(mensajePedido);
            await sender.SendMessageAsync(message);

            return pedido;
        }
    }
}
