#region Usings

using System.ComponentModel.Composition;
using System.Threading.Tasks;
using LOB.Log.Interface;
using Microsoft.Practices.Prism.Logging;
using log4net;
using log4net.Config;

#endregion

namespace LOB.Log {
    /// <summary>
    ///     A log4Net implementation of PRISM' ILoggerFacade.
    ///     *Note: Any logging framework could be plugged in here as long as we implement the ILoggerFacade interface.
    /// </summary>
    [Export(typeof(ILoggerFacade))]
    public class Logger : ILoggerFacade, ILogger {
        // Member variables
// ReSharper disable InconsistentNaming
        private static readonly ILog _logger = LogManager.GetLogger(typeof(Logger));
// ReSharper restore InconsistentNaming

        public Logger() { XmlConfigurator.Configure(); }
        #region ILoggerFacade Members

        /// <summary>
        ///     Prism Log routine.
        /// </summary>
        /// <param name="message">The message to write.</param>
        /// <param name="category">The message category.</param>
        /// <param name="priority">Not used by Log4Net; pass Priority.None.</param>
        public void Log(string message, Category category, Priority priority) {
            Task.Run(() => {
                         switch(category) {
                             case Category.Debug:
                                 _logger.Debug(message);
                                 break;

                             case Category.Warn:
                                 _logger.Warn(message);
                                 break;

                             case Category.Exception:
                                 _logger.Error(message);
                                 break;

                             case Category.Info:
                                 _logger.Info(message);
                                 break;
                         }
                     });
        }

        #endregion ILoggerFacade Members
    }
}