#region Usings

using System.ComponentModel.Composition;
using System.Windows.Input;

#endregion

namespace LOB.UI.Interface.Command
{
    [InheritedExport]
    public interface ICommandService
    {
        ICommand this[string name] { get; }
        void RegisterCommand(string param, ICommand command);
    }
}