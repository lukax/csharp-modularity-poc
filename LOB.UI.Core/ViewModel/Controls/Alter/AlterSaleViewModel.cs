#region Usings

using System.ComponentModel.Composition;
using LOB.Domain;
using LOB.UI.Contract.ViewModel.Controls.Alter;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter {
    [Export(typeof(IAlterSaleViewModel)), PartCreationPolicy(CreationPolicy.NonShared)]
    public sealed class AlterSaleViewModel : AlterBaseEntityViewModel<Order>, IAlterSaleViewModel {}
}