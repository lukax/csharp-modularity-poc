#region Usings

using System.Windows;
using System.Windows.Interactivity;

#endregion

namespace MahApps.Metro.Behaviours
{
    public class StylizedBehaviorCollection : FreezableCollection<Behavior>
    {
        protected override Freezable CreateInstanceCore() {
            return new StylizedBehaviorCollection();
        }
    }
}