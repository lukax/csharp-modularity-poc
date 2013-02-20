#region Usings

using System.ComponentModel.Composition;

#endregion

namespace LOB.UI.Interface
{
    [InheritedExport]
    public interface IRegionAdapter
    {
        IRegionAdapter RegisterRegion(string name, object region);
        IRegionAdapter AddView<TView>(TView view, string regionName, string title = "IsDefault") where TView : class;
    }
}