using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SampleCqrsDapperApi.Domain.Entities;
using SampleCqrsDapperApi.Domain.Interfaces;
using System.Data;

namespace SampleCqrsDapperApi.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly string _connectionString;

    public OrderRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    private IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }

    public async Task<int> CreateOrderAsync(Order order)
    {
        using var connection = CreateConnection();
        const string sql = @"
            INSERT INTO Orders (CustomerId, OrderDate, TotalAmount)
            VALUES (@CustomerId, @OrderDate, @TotalAmount);
        ";
        return await connection.ExecuteAsync(sql, order);
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        using var connection = CreateConnection();
        const string sql = "SELECT * FROM Orders";
        return await connection.QueryAsync<Order>(sql);
    }

    public async Task<bool> UpdateOrderCustomerAsync(int orderId, int newCustomerId)
    {
        using var connection = CreateConnection();
        const string sql = @"
            UPDATE Orders
            SET CustomerId = @NewCustomerId
            WHERE Id = @OrderId;
        ";
        var affectedRows = await connection.ExecuteAsync(sql, new { OrderId = orderId, NewCustomerId = newCustomerId });
        return affectedRows > 0;
    }
}
