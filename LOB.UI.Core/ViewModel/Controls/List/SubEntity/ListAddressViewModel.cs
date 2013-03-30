#region Usings
using System;
using System.Linq.Expressions;
using LOB.Dao.Interface;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List.SubEntity;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List.SubEntity {
    public class ListAddressViewModel : ListBaseEntityViewModel<Address>, IListAddressViewModel {

        public ListAddressViewModel(Address entity, IRepository repository, IEventAggregator eventAggregator)
            : base(entity, repository, eventAggregator) {}

        public new Expression<Func<Address, bool>> SearchCriteria {
            get {
                try {
                    return
                        (arg =>
                         arg.Code.ToString().ToUpper().Contains(this.Search.ToUpper()) ||
                         arg.City.ToUpper().Contains(this.Search.ToUpper()) ||
                         arg.Country.ToUpper().Contains(this.Search.ToUpper()) ||
                         arg.District.ToString().ToUpper().Contains(this.Search.ToUpper()) ||
                         arg.Street.ToString().ToUpper().Contains(this.Search.ToUpper()) ||
                         arg.StreetComplement.ToString().ToUpper().Contains(this.Search.ToUpper()) ||
                         arg.StreetNumber.ToString().ToUpper().Contains(this.Search.ToUpper()) ||
                         arg.ZipCode.ToString().ToUpper().Contains(this.Search.ToUpper()) ||
                         arg.State.ToString().ToUpper().Contains(this.Search.ToUpper()) ||
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
            get { return OperationType.ListAddress; }
        }

    }
}