using Microsoft.Data.SqlClient;

namespace DAL.DbContextFactory
{
    public interface IDbConnectionFactory
    {
        SqlConnection CreateConnection();
    }
}