using System.Data;

namespace Comanda.Infra.ConnectionFactory.Interfaces
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
