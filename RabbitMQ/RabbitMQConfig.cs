using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace unwallet.RabbitMQ;

public static class RabbitMQConfiguration{
    public static ConnectionFactory ConfigureConnectionFactory(){
        ConnectionFactory factory = new ConnectionFactory(){
            HostName = "localhost",
            Port = 5672,
            UserName = "guest",
            Password = "guest"
        };
        return factory;
    }    
};
