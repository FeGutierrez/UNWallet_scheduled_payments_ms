using unwallet.Models;
using unwallet.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using unwallet.RabbitMQ;
using Newtonsoft.Json;

using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<MongoDBService>();


/*var factory = RabbitMQConfiguration.ConfigureConnectionFactory();
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();
var rpcQueueName = "rpc_queue";
channel.QueueDeclare(queue: rpcQueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
var consumer = new EventingBasicConsumer(channel);
consumer.Received += async (sender, args) => {
    var message = Encoding.UTF8.GetString(args.Body.ToArray());
    var n = int.Parse(message);
    var serviceProvider = builder.Services.BuildServiceProvider();
    var service = serviceProvider.GetRequiredService<MongoDBService>();

    var response = await service.GetListFromIntegerAsync(n);

    //Publish the response
    var replyProperties = channel.CreateBasicProperties();
    replyProperties.CorrelationId = args.BasicProperties.CorrelationId;
    var responseBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response));
    channel.BasicPublish(exchange: "", routingKey: args.BasicProperties.ReplyTo, basicProperties: replyProperties, body: responseBytes);

    channel.BasicAck(args.DeliveryTag, false);

};*/

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
