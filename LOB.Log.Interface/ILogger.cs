#region Usings

using Microsoft.Practices.Prism.Logging;

#endregion

namespace LOB.Log.Contract {
    public interface ILogger {
        void Log(string message, Category category, Priority priority);
    }
}