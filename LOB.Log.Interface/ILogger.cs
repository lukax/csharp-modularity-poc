#region Usings

using Microsoft.Practices.Prism.Logging;

#endregion

namespace LOB.Log.Interface {
    public interface ILogger {
        void Log(string message, Category category, Priority priority);
    }
}