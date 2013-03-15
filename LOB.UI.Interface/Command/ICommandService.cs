#region Usings

using System.Collections.Generic;
using System.Windows.Input;
using LOB.UI.Interface.Names;

#endregion

namespace LOB.UI.Interface.Command
{
    public interface ICommandService
    {
        void Register<T>(T token, ICommand command);
        void Execute<T>(T token, object arg);
        IEnumerable<ICommand> this[string token] { get; }
    }
}