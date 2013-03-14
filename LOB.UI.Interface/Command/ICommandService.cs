#region Usings

using System.Windows.Input;
using LOB.UI.Interface.Names;

#endregion

namespace LOB.UI.Interface.Command
{
    public interface ICommandService
    {
        void RegisterCommand<T>(T token, ICommand command);
        void ExecuteCommand<T>(T token, object arg);
        ICommand this[string token] { get; }
    }
}