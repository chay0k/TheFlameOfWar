using Contracts.Models;
using Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Core.Commands.Tests
{
    public class NameTests
    {
        [Fact]
        public async Task ExecuteAsync_NoPlayerName_ReturnsRequestNameMessage()
        {
            // Arrange
            var commandServiceMock = new Mock<ICommandService>();
            var playerServiceMock = new Mock<IPlayerService>();
            var sessionServiceMock = new Mock<ISessionService>();

            // Configure the behavior of the mocks
            sessionServiceMock.Setup(s => s.SessionPlayer).Returns((Player)null);
            sessionServiceMock.Setup(s => s.LastInput).Returns("");
            sessionServiceMock.Setup(s => s.UserTelegramId).Returns(1);

            var serviceProvider = new ServiceCollection()
                .AddScoped<ICommandService>(provider => commandServiceMock.Object)
                .AddScoped<IPlayerService>(provider => playerServiceMock.Object)
                .AddScoped<ISessionService>(provider => sessionServiceMock.Object)
                .BuildServiceProvider();

            var nameCommand = new Name(serviceProvider);

            // Act
            var result = await nameCommand.ExecuteAsync();

            // Assert
            Assert.Equal("Please enter your name", result);
        }
    }
}

