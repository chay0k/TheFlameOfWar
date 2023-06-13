using Contracts;
using Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Core.Commands
{
    public class CreateMap : ICommand
    {
        private readonly IMapService _mapService;
        private readonly ILobbyService _lobbyService;
        private readonly ISessionService _sessionService;

        public CreateMap(IMapService mapService, ILobbyService lobbyService, ISessionService sessionService)
        {
            _mapService = mapService;
            _lobbyService = lobbyService;
            _sessionService = sessionService;
        }

        public async Task<string> ExecuteAsync()
        {
            var result = "";

            if (_sessionService != null)
            {
                // Ваш код
            }

            var randomMap = await _mapService.CreateRandomMapAsync();
            _sessionService.Lobby.Map = randomMap;
            result = "New map created and added to lobby";
            return result;
        }
    }
}
