using RabbitMQ.Client;

namespace ConsultCep.Domain.Interfaces
{
    public interface IMessengerRepository
    {
        void InviteMessage(object message, ConnectionFactory factory);
    }
}
