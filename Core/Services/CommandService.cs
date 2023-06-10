using Contracts;
using Contracts.Models;
using Core.Commands;
using Core.Commands.LobbyCommands;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Services
{
    public class CommandService : ICommandService
    {
        public Guid Id { get; set; }
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<string, Func<ICommand>> _commands;
        private readonly Stack<(ICommand, string)> _commandsStack;

        public CommandService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _commands = new Dictionary<string, Func<ICommand>>
            {
                { Constants.Start,          () => new StartCommand() },
                { Constants.HotSeat,        () => new HotSeat(_serviceProvider) },
                { Constants.Continue,       () => new BackToCurrentGame() },
                { Constants.NewGame,        () => new NewGame(_serviceProvider) },
                { Constants.Connect,        () => new ConnectToExist(_serviceProvider) },
                { Constants.MapChoose,      () => new ChooseMap() },
                { Constants.MapCreate,      () => new CreateMap() },
                { Constants.Info,           () => new Info() },
                { Constants.Back,           () => new StepBack() },
                { Constants.Menu,           () => new MainMenu() },
                { Constants.Name,           () => new Name(_serviceProvider) },
            };

            _commandsStack = new Stack<(ICommand, string)>();
            Id = Guid.NewGuid();
            _commandsStack = new Stack<(ICommand, string)>();
        }

        public bool ExpectedInput { get; set; } = false;

        public ICommand FindCommand(ref string text)
        {
            var command = FindCommandEnum(text);
            if (command != null && _commands.TryGetValue(command, out var commandFactory))
            {
                text = text.Replace(command, "").TrimStart(' ');
                return commandFactory.Invoke();
            }
            else
                return null;
        }


        public string FindCommandEnum(string text)
        {
            var firstWord = text.Split(' ')[0].ToLower();
            return firstWord;
        }

        public (ICommand, string) PeekCommand()
        {
            if (_commandsStack.Count() == 0)
                return (null, "");
            var command = _commandsStack.Peek();
            return (command.Item1, command.Item2);
        }


        public void PushCommand(ICommand command, string argument)
        {
            if (_commandsStack.Count > 0 && _commandsStack.Peek().Item1 == command)
                return;
            _commandsStack.Push((command, argument));
        }


        public (ICommand, string) PopCommand()
        {
            if (_commandsStack.Count() == 0)
                return (null, "");
            return _commandsStack.Pop();
        }


        public void ClearCommands()
        {
            _commandsStack.Clear();
        }
    }
}
