#region Usings

using System.ComponentModel.Composition;
using LOB.Domain;
using LOB.UI.Interface.ViewModel.Controls.Alter.Base;

#endregion

namespace LOB.UI.Interface.ViewModel.Controls.Alter
{
    [InheritedExport]
    public interface IAlterProductViewModel : IAlterServiceViewModel<Product>
    {
    }
}