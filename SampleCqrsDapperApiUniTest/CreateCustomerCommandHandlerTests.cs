using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using SampleCqrsDapperApi.Application.Commands;
using SampleCqrsDapperApi.Application.Handlers;
using SampleCqrsDapperApi.Domain.Entities;
using SampleCqrsDapperApi.Domain.Interfaces;
using Xunit;

namespace SampleCqrsDapperApiUniTest.Handlers
{
    public class CreateCustomerCommandHandlerTests
    {
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly CreateCustomerCommandHandler _handler;

        public CreateCustomerCommandHandlerTests()
        {
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _handler = new CreateCustomerCommandHandler(_customerRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnCustomerId_WhenCreateSuccess()
        {
            // Arrange
            var command = new CreateCustomerCommand
            {
                Name = "Josh",
                Email = "josh@example.com"
            };

            _customerRepositoryMock
                .Setup(repo => repo.CreateCustomerAsync(It.IsAny<Customer>()))
                .ReturnsAsync(123); // Mock ให้สมมุติว่าคืน id = 123

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(123);
            _customerRepositoryMock.Verify(repo => repo.CreateCustomerAsync(It.IsAny<SampleCqrsDapperApi.Domain.Entities.Customer>()), Times.Once);
        }
    }
}
