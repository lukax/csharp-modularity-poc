#region Usings

using System.Windows;

#endregion

namespace LOB.UI.Core.View
{
    public sealed partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            new Bootstrapper().Run();
        }
    }
}