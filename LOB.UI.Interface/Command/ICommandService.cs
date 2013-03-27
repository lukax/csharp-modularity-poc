#region Usings

using System.Collections.Generic;
using System.Windows.Input;

#endregion

namespace LOB.UI.Interface.Command {
    public interface ICommandService {
        IEnumerable<ICommand> this[string token] { get; }
        void Register<T>(T token, ICommand command);
        void Execute<T>(T token, object arg);
    }
}