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

        [InjectionConstructor] public ListProductViewModel(Product entity, IRepository repository,
            EventAggregator eventAggregator)
            : base(entity, repository, eventAggregator) {}

        public new Expression<Func<Product, bool>> SearchCriteria {
            get {
                try {
                    return
                        (arg =>
                         arg.Code.ToString().ToUpper().Contains(this.Search.ToUpper()) ||
                         arg.Name.ToUpper().Contains(this.Search.ToUpper()) ||
                         arg.Description.ToUpper().Contains(this.Search.ToUpper()) ||
                         arg.UnitSalePrice.ToString().ToUpper().Contains(this.Search.ToUpper()) ||
                         arg.ProfitMargin.ToString().ToUpper().Contains(this.Search.ToUpper()) ||
                         arg.Status.ToString().ToUpper().Contains(this.Search.ToUpper()));
                }
                catch(FormatException) {
                    return arg => false;
                }
            }
        }

        public override void Refresh() {
            throw new NotImplementedException();
        }

        public override OperationType OperationType {
            get { return OperationType.ListProduct; }
        }

        protected override bool CanUpdate(object arg) {
            //TODO: Business logic
            return true;
        }

        protected override bool CanDelete(object arg) {
            //TODO: Business logic
            return true;
        }

    }
}