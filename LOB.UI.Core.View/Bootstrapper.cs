using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Prism.Modularity;

namespace LOB.UI.Core.View
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override IModuleCatalog CreateModuleCatalog()
        {
            return (Microsoft.Practices.Prism.Modularity
                .ModuleCatalog.CreateFromXaml(new Uri("ModuleCatalog.xaml",
                                                         UriKind.Relative)));
        }
        
        protected override DependencyObject CreateShell()
        {
            return null;
        }
    }
}
