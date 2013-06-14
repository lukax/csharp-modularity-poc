#region Usings

using System;
using System.ComponentModel.Composition;
using System.Linq.Expressions;
using LOB.Domain.SubEntity;
using LOB.UI.Contract.ViewModel.Controls.List.SubEntity;
using LOB.UI.Core.ViewModel.Controls.List.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List.SubEntity {
    [Export(typeof(IListPayCheckViewModel)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class ListPayCheckViewModel : ListBaseEntityViewModel<Paycheck>, IListPayCheckViewModel {
        public override Expression<Func<Paycheck, bool>> SearchCriteria {
            get {
                try {
                    return
                        (arg =>
                         arg.Code.ToString(Culture).ToUpper().Contains(SearchString.ToUpper()) ||
                         arg.Detail.ToString(Culture).ToUpper().Contains(SearchString.ToUpper()) ||
                         arg.Bonus.ToString(Culture).ToUpper().Contains(SearchString.ToUpper()) ||
                         arg.CurrentSalary.ToString(Culture).ToUpper().Contains(SearchString.ToUpper()));
                } catch(FormatException) {
                    return arg => false;
                }
            }
        }
    }
}