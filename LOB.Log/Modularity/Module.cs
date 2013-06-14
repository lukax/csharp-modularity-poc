#region Usings

using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;

#endregion

namespace LOB.Log.Modularity {
    [ModuleExport("LogModule", typeof(Module))]
    public class Module : IModule {
        public void Initialize() { }
    }
}