#region Usings

using System;
using System.ComponentModel.Composition;
using System.Linq.Expressions;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using LOB.UI.Interface.ViewModel.Controls.List;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List {
    [Export(typeof(IListProductViewModel))]
    public sealed class ListProductViewModel : ListBaseEntityViewModel<Product>, IListProductViewModel {
        [ImportingConstructor]
        public ListProductViewModel(IRepository repository, EventAggregator eventAggregator)
            : base(repository, eventAggregator) { }

        public new Expression<Func<Product, bool>> SearchCriteria {
            get {
                try {
                    return
                        (arg =>
                         arg.Code.ToString(Culture).ToUpper().Contains(Search.ToUpper()) || arg.Name.ToUpper().Contains(Search.ToUpper()) ||
                         arg.Description.ToUpper().Contains(Search.ToUpper()) ||
                         arg.UnitSalePrice.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.ProfitMargin.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.Status.ToString().ToUpper().Contains(Search.ToUpper()));
                } catch(FormatException) {
                    return arg => false;
                }
            }
        }
    }
}