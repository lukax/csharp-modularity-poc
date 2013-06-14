#region Usings

using System.ComponentModel.Composition;
using LOB.UI.Contract.ViewModel.Controls.Sell;
using LOB.UI.Core.ViewModel.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Sell {
    [Export(typeof(ISellServiceViewModel))]
    public class SellServiceViewModel : BaseViewModel, ISellServiceViewModel {}
}