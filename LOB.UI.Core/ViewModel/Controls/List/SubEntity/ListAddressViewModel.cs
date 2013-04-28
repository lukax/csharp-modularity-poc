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
    }
}