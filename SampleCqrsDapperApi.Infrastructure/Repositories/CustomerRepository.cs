using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SampleCqrsDapperApi.Domain.Entities;
using SampleCqrsDapperApi.Domain.Interfaces;
using System.Data;

namespace SampleCqrsDapperApi.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly string _connectionString;

    public CustomerRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    private IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }

    public async Task<int> CreateCustomerAsync(Customer customer)
    {
        using var connection = CreateConnection();
        const string sql = "INSERT INTO Customers (Name, Email) OUTPUT INSERTED.Id VALUES (@Name, @Email)";
        return await connection.ExecuteScalarAsync<int>(sql, customer);
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        using var connection = CreateConnection();
        const string sql = "SELECT * FROM Customers";
        return await connection.QueryAsync<Customer>(sql);
    }

    public async Task<IEnumerable<(Customer, Order)>> GetCustomersWithOrdersAsync()
    {
        using var connection = CreateConnection();
        const string sql = @"
            SELECT 
                c.Id, c.Name, c.Email, 
                o.Id, o.OrderDate, o.TotalAmount, o.CustomerId
            FROM Customers c
            LEFT JOIN Orders o ON c.Id = o.CustomerId;
        ";

        return await connection.QueryAsync<Customer, Order, (Customer, Order)>(
            sql,
            (customer, order) => (customer, order),
            splitOn: "Id"
        );
    }
}
