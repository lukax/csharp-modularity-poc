#region Usings

using System.Windows.Input;

#endregion

namespace LOB.UI.Interface.Command
{
    public interface ICommandService
    {
        ICommand this[string name] { get; }
        void RegisterCommand(string param, ICommand command);
    }
}