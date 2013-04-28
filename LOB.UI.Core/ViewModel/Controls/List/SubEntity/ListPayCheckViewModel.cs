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
    public class ListPayCheckViewModel : ListBaseEntityViewModel<PayCheck>, IListPayCheckViewModel {
        public override Expression<Func<PayCheck, bool>> SearchCriteria {
            get {
                try {
                    return
                        (arg =>
                         arg.Code.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.PS.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.Bonus.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.CurrentSalary.ToString(Culture).ToUpper().Contains(Search.ToUpper()));
                } catch(FormatException) {
                    return arg => false;
                }
            }
        }
    }
}