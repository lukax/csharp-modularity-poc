#region Usings

using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;

#endregion

namespace LOB.Dao.Nhibernate.Modularity {
    [ModuleExport("DaoModule", typeof(Module), DependsOnModuleNames = new[] {"LogModule"})]
    public class Module : IModule {
#if DEBUG
        [Import] public ILoggerFacade LoggerFacade { get; set; }
#endif

        public void Initialize() {
#if DEBUG
            LoggerFacade.Log("NhibernateModule Initialized", Category.Debug, Priority.Medium);
#endif
        }
    }
}