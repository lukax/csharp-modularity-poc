#region Usings

using System;
using System.Collections.Generic;
using System.Windows.Input;
using LOB.UI.Interface.Command;

#endregion

namespace LOB.UI.Core
{
    public class CommandService : ICommandService
    {
        private static readonly Lazy<ICommandService> Lazy = new Lazy<ICommandService>(() => new CommandService());

        private IDictionary<string, ICommand> commandMap;

        private CommandService()
        {
            commandMap = new Dictionary<string, ICommand>();
        }

        public static ICommandService Default
        {
            get { return Lazy.Value; }
        }

        public void RegisterCommand(string param, ICommand command)
        {
            commandMap.Add(param, command);
        }

        public ICommand this[string name]
        {
            get { return commandMap[name]; }
        }
    }
}