# DemoServiceBus
## Demo para ejemplo de Service Bus.

Ejemplo basico donde se muestra como implementar un Azure Service Bus.

![Acceso](/DemoServiceBus.PNG)


Implementar el envío de mensajes en una Api Rest
``` 
// Eviar a Service Bus
var queueName = _configuration["ServiceBus:queueName"];
var connectionService = _configuration["ServiceBus:connectionString"];

await using var client = new ServiceBusClient(connectionService);
ServiceBusSender sender = client.CreateSender(queueName);

string mensajePedido= JsonSerializer.Serialize(pedido);
ServiceBusMessage message = new ServiceBusMessage(mensajePedido);
await sender.SendMessageAsync(message);
```
