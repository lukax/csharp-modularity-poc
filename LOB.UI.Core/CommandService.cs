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
        private IDictionary<string, string> regionMap;

        private CommandService()
        {
            commandMap = new Dictionary<string, ICommand>();
            regionMap = new Dictionary<string, string>();
        }

        public static ICommandService Default
        {
            get { return Lazy.Value; }
        }

        public void RegisterCommand(string opName, string regionName, ICommand command)
        {
            commandMap.Add(opName, command);
        }

        public IRegionedCommand this[string name]
        {
            get { return new RegionedCommand { Command = commandMap[name], Region = regionMap[name] }; }
        }

        public class RegionedCommand : IRegionedCommand
        {
            public ICommand Command { get; set; }
            public string Region { get; set; }
        }
    }
}