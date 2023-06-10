using Contracts;
using Contracts.Models;
using Core.Services;
using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Core.Commands.Tests
{
    public class NameTests
    {
        [Fact]
        public async Task ExecuteAsync_NoPlayerName_ReturnsRequestNameMessage()
        {
            // Arrange
            var services = new ServiceCollection();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<UnitOfWork>();

            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IMapService, MapService>();
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<ILobbyService, LobbyService>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<ICommandService, CommandService>();
            services.AddScoped<IResultPresenter, TestResultPresenter>();

            //services.AddScoped<ConsoleEvents>();

            var serviceProvider = services.BuildServiceProvider();

            //var session = serviceProvider.GetRequiredService<ISessionService>();
            var sessionMock = new Mock<ISessionService>();
            sessionMock.Setup(s => s.SessionPlayer).Returns((Player)null);
            sessionMock.Setup(s => s.LastInput).Returns("");

            var nameCommand = new Name(serviceProvider);

            // Act
            var result = await nameCommand.ExecuteAsync(sessionMock.Object);

            // Assert
            Assert.Equal("Please enter your name", result);
        }
    }
}
