using DAL.DbContextFactory;
using DAL.Interfaces;
using Microsoft.Data.SqlClient;
using Model;
using Model.Customer;
using System.Data;

namespace DAL.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public CustomerRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<CustomerModel>> GetOnlyTvCustomerAsync()
        {
            using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync();

            var query = @"
                SELECT DISTINCT c.*
                FROM Customer c
                WHERE EXISTS (
                    SELECT 1 FROM TvProducts tv
                    WHERE tv.CustomerId = c.Id
                    AND tv.StartDate < GETDATE()
                    AND (tv.EndDate IS NULL OR tv.EndDate > GETDATE())
                )
                AND NOT EXISTS (
                SELECT 1 FROM DslProducts dsl
                WHERE dsl.CustomerId = c.Id
                    AND dsl.StartDate < GETDATE()
                    AND (dsl.EndDate IS NULL OR dsl.EndDate > GETDATE())
                );";

            using var command = new SqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();

            var tvCustomers = new List<CustomerModel>();

            while (await reader.ReadAsync())
            {
                tvCustomers.Add(new CustomerModel
                {
                    Id = reader.GetGuid(0),
                    Email = reader.GetString(1),
                    Address = reader.GetString(2)
                });
            }

            return tvCustomers;
        }

        public async Task<IEnumerable<CustomerModel>> GetOnlyDslCustomerAsync()
        {
            using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync();

            var query = @"SELECT DISTINCT c.*
                FROM Customer c
                WHERE EXISTS (
                    SELECT 1 FROM DslProducts dsl
                    WHERE dsl.CustomerId = c.Id
                    AND dsl.StartDate < GETDATE()
                    AND (dsl.EndDate IS NULL OR dsl.EndDate > GETDATE())
                )
                AND NOT EXISTS (
                SELECT 1 FROM TvProducts tv
                WHERE tv.CustomerId = c.Id
                    AND tv.StartDate < GETDATE()
                    AND (tv.EndDate IS NULL OR tv.EndDate > GETDATE())
                );";

            using var command = new SqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();

            var dslCustomers = new List<CustomerModel>();

            while (await reader.ReadAsync())
            {
                dslCustomers.Add(new CustomerModel
                {
                    Id = reader.GetGuid(0),
                    Email = reader.GetString(1),
                    Address = reader.GetString(2)
                });
            }

            return dslCustomers;
        }

        public async Task<IEnumerable<OverlappingTvProductsModel>> GetOverlappingTvProducts()
        {
            using var connection = _connectionFactory.CreateConnection();

            await connection.OpenAsync();
            using var command = new SqlCommand("GetOverlappingTvProducts", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            using var reader = await command.ExecuteReaderAsync();
            var overlappingTvProducts = new List<OverlappingTvProductsModel>();

            while (await reader.ReadAsync())
            {
                var model = new OverlappingTvProductsModel
                {
                    ProductId1 = reader.GetGuid(0),
                    CustomerId = reader.GetGuid(1),
                    Email = reader.GetString(2),
                    Address = reader.GetString(3),
                    ProductName1 = reader.GetString(4),
                    StartDate1 = reader.GetDateTime(5),
                    EndDate1 = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6),
                    ProductId2 = reader.GetGuid(7),
                    ProductName2 = reader.GetString(8),
                    StartDate2 = reader.GetDateTime(9),
                    EndDate2 = reader.IsDBNull(10) ? (DateTime?)null : reader.GetDateTime(10),
                };
                overlappingTvProducts.Add(model);
            }
            return overlappingTvProducts;
        }
        public async Task<IEnumerable<MatchedCustomerModel>> FindMatchedCustomer()
        {
            using var connection = _connectionFactory.CreateConnection();

            await connection.OpenAsync();
            var query = @"SELECT DISTINCT
                            c1.Id AS CustomerIdTv,
                            c2.Id AS CustomerIdDsl,
                            tv.StartDate
                        FROM TvProducts tv
                        JOIN Customer c1 ON tv.CustomerId = c1.Id
                        JOIN DslProducts dsl ON 1 = 1
                        JOIN Customer c2 ON dsl.CustomerId = c2.Id
                        WHERE 
                            c1.Email = c2.Email
                            AND c1.Address = c2.Address
                            AND c1.Id <> c2.Id
                        ORDER BY tv.StartDate;";

            using var command = new SqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();

            var matchedCustomers = new List<MatchedCustomerModel>();

            while (await reader.ReadAsync())
            {
                matchedCustomers.Add(new MatchedCustomerModel
                {
                    CustomerTvId = reader.GetGuid(0),
                    CustomerDslId = reader.GetGuid(1),
                    StartDate = reader.GetDateTime(2)
                });
            }
            return matchedCustomers;
        }
        public async Task PostMatchedCustomersAsync(IEnumerable<MatchedCustomerModel> models)
        {
            using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync();

            foreach (var model in models)
            {
                using var command = new SqlCommand("PostMatchedCustomer", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@CustomerDslId", model.CustomerDslId);
                command.Parameters.AddWithValue("@CustomerTvId", model.CustomerTvId);
                command.Parameters.AddWithValue("@StartDate", model.StartDate);

                await command.ExecuteNonQueryAsync();
            }
        }
        public async Task<IEnumerable<MatchedCustomerModel>> GetMatchedCustomer()
        {
            using var connection = _connectionFactory.CreateConnection();

            await connection.OpenAsync();
            var query = @"Select CustomerTvId, 
                          CustomerDslId, StartDate 
                          from MatchedCustomers";

            using var command = new SqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();

            var matchedCustomers = new List<MatchedCustomerModel>();

            while (await reader.ReadAsync())
            {
                matchedCustomers.Add(new MatchedCustomerModel
                {

                    CustomerTvId = reader.GetGuid(0),
                    CustomerDslId = reader.GetGuid(1),
                    StartDate = reader.GetDateTime(2)
                });
            }
            return matchedCustomers;
        }
    }
}
