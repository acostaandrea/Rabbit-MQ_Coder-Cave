using System.Text;
using RabbitMQ.Client;

class Program
{
    static void Main(string[] args)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        {
            using (var channel = connection.CreateModel())
            {
                //channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);
                //string message = "Hello World!";
                channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);
                string message = GetMessage(args);
                var body = Encoding.UTF8.GetBytes(message);
                //channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body);
                channel.BasicPublish(exchange: "logs", routingKey: "", basicProperties: null, body: body);
                Console.WriteLine($" [x] Sent {message}");
            }
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();

        }
    }
    private static string GetMessage(string[] args)
    {
        return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
    }
}




