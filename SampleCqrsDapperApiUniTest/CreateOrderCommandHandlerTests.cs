using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using SampleCqrsDapperApi.Application.Commands;
using SampleCqrsDapperApi.Application.Handlers;
using SampleCqrsDapperApi.Domain.Entities;
using SampleCqrsDapperApi.Domain.Interfaces;
using Xunit;

namespace SampleCqrsDapperApiUniTest
{
    public class CreateOrderCommandHandlerTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly CreateOrderCommandHandler _handler;

        public CreateOrderCommandHandlerTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _handler = new CreateOrderCommandHandler(_orderRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnOrderId_WhenCreateSuccess()
        {
            // Arrange
            var command = new CreateOrderCommand
            {
                CustomerId = 1,
                OrderDate = new DateTime(2025, 6, 8),
                TotalAmount = 999.99m
            };

            _orderRepositoryMock
                .Setup(repo => repo.CreateOrderAsync(It.IsAny<Order>()))
                .ReturnsAsync(456); // Mock คืน id = 456

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(456);
            _orderRepositoryMock.Verify(repo => repo.CreateOrderAsync(It.IsAny<Order>()), Times.Once);
        }
    }
}
