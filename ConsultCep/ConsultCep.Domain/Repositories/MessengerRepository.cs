using ConsultCep.Domain.Interfaces;
using ConsultCep.Utils.ClassUtils;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultCep.Domain.Repositories
{
    public class MessengerRepository : IMessengerRepository
    {
        public void InviteMessage(object message, ConnectionFactory factory)
        {

            using var connection = factory.CreateConnection();

            using var channel = connection.CreateModel();
            var properties = channel.CreateBasicProperties();
            var convert = ConvertObjectToByte.ObjectToByteArray(message);

            ///////Topic com # para envio a todos com teste. qualquer coisa
            //channel.ExchangeDeclare
            //    (
            //    "testeExchange2",
            //    "topic",
            //    true
            //    );
            //channel.BasicPublish
            //    (
            //    "testeExchange2",
            //    "teste.asdasdasdsa",
            //    body: convert
            //    );

            /// Direct enviando apenas a aquela queue
            channel.QueueDeclare
               (
                "direct",
                durable: true,
                autoDelete: false,
                arguments: null
                );

             channel.BasicPublish
               (
               exchange: "DirectExchange",
                routingKey: "123",
                basicProperties: null,
                body: convert
              );

            //////Fanout
            //channel.ExchangeDeclare
            //    (
            //    "FanoutExchange",
            //    "fanout",
            //  durable: true
            //    );

            //channel.BasicPublish
            //    (
            //    "FanoutExchange",
            //    "",
            //   properties,
            //    convert
            //    );
        }

    }
}
