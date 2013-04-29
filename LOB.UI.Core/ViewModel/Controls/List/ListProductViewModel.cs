#region Usings

using System;
using System.ComponentModel.Composition;
using System.Linq.Expressions;
using LOB.Domain;
using LOB.UI.Contract.ViewModel.Controls.List;
using LOB.UI.Core.ViewModel.Controls.List.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List {
    [Export(typeof(IListProductViewModel)), PartCreationPolicy(CreationPolicy.NonShared)]
    public sealed class ListProductViewModel : ListBaseEntityViewModel<Product>, IListProductViewModel {
        public override Expression<Func<Product, bool>> SearchCriteria {
            get {
                try {
                    return
                        (arg =>
                         arg.Code.ToString(Culture).ToUpper().Contains(SearchString.ToUpper()) || arg.Name.ToUpper().Contains(SearchString.ToUpper()) ||
                         arg.Description.ToUpper().Contains(SearchString.ToUpper()) ||
                         arg.UnitSalePrice.ToString(Culture).ToUpper().Contains(SearchString.ToUpper()) ||
                         arg.ProfitMargin.ToString(Culture).ToUpper().Contains(SearchString.ToUpper()) ||
                         arg.Status.ToString().ToUpper().Contains(SearchString.ToUpper()));
                } catch(FormatException) {
                    return arg => false;
                }
            }
        }
    }
}