#region Usings

using System.Windows;
using LOB.UI.Core.View.Modularity;

#endregion

namespace LOB.UI.Core.View {
    public sealed partial class App : Application {

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);
            new Bootstrapper().Run();
        }

    }
}