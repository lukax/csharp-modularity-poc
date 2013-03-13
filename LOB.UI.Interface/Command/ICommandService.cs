#region Usings

using System.Windows.Input;

#endregion

namespace LOB.UI.Interface.Command
{
    public interface ICommandService
    {
        IRegionedCommand this[string name] { get; }
        void RegisterCommand(string opName, string regionName, ICommand command);
    }

    public interface IRegionedCommand
    {
         ICommand Command { get; set; }
         string Region { get; set; }
    }
}