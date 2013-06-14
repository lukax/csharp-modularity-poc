#region Usings

using System;
using System.ComponentModel.Composition;
using System.Linq.Expressions;
using LOB.Domain.SubEntity;
using LOB.UI.Contract.ViewModel.Controls.List.SubEntity;
using LOB.UI.Core.ViewModel.Controls.List.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List.SubEntity {
    [Export(typeof(IListAddressViewModel)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class ListAddressViewModel : ListBaseEntityViewModel<Address>, IListAddressViewModel {
        public override Expression<Func<Address, bool>> SearchCriteria {
            get {
                try {
                    return
                        (arg =>
                         arg.Code.ToString(Culture).ToUpper().Contains(SearchString.ToUpper()) || arg.County.ToUpper().Contains(SearchString.ToUpper()) ||
                         arg.Country.ToUpper().Contains(SearchString.ToUpper()) || arg.District.ToString(Culture).ToUpper().Contains(SearchString.ToUpper()) ||
                         arg.Street.ToString(Culture).ToUpper().Contains(SearchString.ToUpper()) ||
                         arg.StreetComplement.ToString(Culture).ToUpper().Contains(SearchString.ToUpper()) ||
                         arg.StreetNumber.ToString(Culture).ToUpper().Contains(SearchString.ToUpper()) ||
                         arg.PostalCode.ToString(Culture).ToUpper().Contains(SearchString.ToUpper()) ||
                         arg.State.ToString(Culture).ToUpper().Contains(SearchString.ToUpper()) ||
                         arg.Status.ToString().ToUpper().Contains(SearchString.ToUpper()));
                } catch(FormatException) {
                    return arg => false;
                }
            }
        }
    }
}