#region Usings

using System;
using System.Linq.Expressions;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List {
    public sealed class ListProductViewModel : ListBaseEntityViewModel<Product>, IListProductViewModel {

        [InjectionConstructor]
        public ListProductViewModel(Product entity, IRepository repository, EventAggregator eventAggregator)
            : base(entity, repository, eventAggregator) { }

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

        public override void InitializeServices() { if (Equals(Operation, default(UIOperation))) Operation = _operation; }

        public override void Refresh() { Search = ""; }

        private readonly UIOperation _operation = new UIOperation {Type = UIOperationType.Product, State = UIOperationState.List};

    }
}