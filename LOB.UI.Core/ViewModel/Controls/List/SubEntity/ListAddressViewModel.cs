#region Usings

using System;
using System.ComponentModel.Composition;
using System.Linq.Expressions;
using LOB.Dao.Interface;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List.SubEntity;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List.SubEntity {
    [Export(typeof(IListAddressViewModel))]
    public class ListAddressViewModel : ListBaseEntityViewModel<Address>, IListAddressViewModel {
        [ImportingConstructor]
        public ListAddressViewModel(IRepository repository, IEventAggregator eventAggregator)
            : base(repository, eventAggregator) { }

        public override void InitializeServices() {
            if(Equals(ViewID, default(ViewID))) ViewID = _defaultViewID;
            base.InitializeServices();
        }

        public new Expression<Func<Address, bool>> SearchCriteria {
            get {
                try {
                    return
                        (arg =>
                         arg.Code.ToString(Culture).ToUpper().Contains(Search.ToUpper()) || arg.County.ToUpper().Contains(Search.ToUpper()) ||
                         arg.Country.ToUpper().Contains(Search.ToUpper()) || arg.District.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.Street.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.StreetComplement.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.StreetNumber.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.ZipCode.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.State.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.Status.ToString().ToUpper().Contains(Search.ToUpper()));
                } catch(FormatException) {
                    return arg => false;
                }
            }
        }

        private readonly ViewID _defaultViewID = new ViewID {Type = ViewType.Address, State = ViewState.List};
    }
}