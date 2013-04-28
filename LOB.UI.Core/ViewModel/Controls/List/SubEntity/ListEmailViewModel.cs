#region Usings

using System;
using System.ComponentModel.Composition;
using System.Linq.Expressions;
using LOB.Domain.SubEntity;
using LOB.UI.Contract.ViewModel.Controls.List.SubEntity;
using LOB.UI.Core.ViewModel.Controls.List.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List.SubEntity {
    [Export(typeof(IListEmailViewModel)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class ListEmailViewModel : ListBaseEntityViewModel<Email>, IListEmailViewModel {
        public override Expression<Func<Email, bool>> SearchCriteria {
            get {
                try {
                    return
                        (arg =>
                         arg.Code.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.Value.ToString(Culture).ToUpper().Contains(Search.ToUpper()));
                } catch(FormatException) {
                    return arg => false;
                }
            }
        }
    }
}