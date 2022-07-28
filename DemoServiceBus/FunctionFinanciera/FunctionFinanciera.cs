using System;
using System.Linq;
using System.Text.Json;
using FunctionFinanciera.Context;
using FunctionFinanciera.Dto;
using FunctionFinanciera.Entities;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace FunctionFinanciera
{
    public class FunctionFinanciera
    {
        private readonly FunctionContext dbContext;
        
        public FunctionFinanciera(FunctionContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [FunctionName("FunctionFinanciera")]
        public void Run([ServiceBusTrigger("NombredelaCola", Connection = "ServiceBusConnectionString")]string myQueueItem, ILogger log)
        {
            try
            {
                Pedido pedido = JsonSerializer.Deserialize<Pedido>(myQueueItem);

                Producto producto = dbContext.Producto.Find(pedido.IdProducto);

                double monto = (pedido.Cantidad * producto.Precio);
                Ganancia ganancia = new Ganancia { IdProducto = producto.IdProducto, Unidades = pedido.Cantidad, Monto = monto };

                dbContext.Ganancia.AddAsync(ganancia);
                dbContext.SaveChanges();

                log.LogInformation($"Se guarda la Ganancia: {producto.Precio}, {pedido.Cantidad}, {monto} ");
            }
            catch (Exception e)
            {
                log.LogCritical(e.Message);
                log.LogCritical(e.StackTrace);
            }
           
        }
    }
}
