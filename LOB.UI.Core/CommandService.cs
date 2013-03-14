#region Usings

using System;
using System.Collections.Generic;
using System.Windows.Input;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.Names;

#endregion

namespace LOB.UI.Core
{
    public class CommandService : ICommandService
    {
        private static readonly Lazy<ICommandService> Lazy = new Lazy<ICommandService>(() => new CommandService());

        private IDictionary<object, ICommand> _commands;
 
        private CommandService()
        {
            _commands = new Dictionary<object, ICommand>();
        }

        public static ICommandService Default
        {
            get { return Lazy.Value; }
        }

        public void RegisterCommand<T>(T token, ICommand command)
        {
            _commands.Add(token, command);
        }

        public void ExecuteCommand<T>(T token, object arg)
        {
            _commands[token].Execute(arg);
        }

        public ICommand this[string token]
        {
            get { return _commands[token]; }
        }
    }
}