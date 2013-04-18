#region Usings

using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Logging;

#endregion

namespace LOB.Log.Interface {
    [InheritedExport]
    public interface ILogger {
        void Log(string message, Category category, Priority priority);
    }
}