#region Usings

using System;
using System.ComponentModel.Composition;
using System.Linq.Expressions;
using LOB.Domain.SubEntity;
using LOB.UI.Contract.ViewModel.Controls.List.SubEntity;
using LOB.UI.Core.ViewModel.Controls.List.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List.SubEntity {
    [Export(typeof(IListCategoryViewModel)), PartCreationPolicy(CreationPolicy.NonShared)]
    public sealed class ListCategoryViewModel : ListBaseEntityViewModel<Category>, IListCategoryViewModel {
        public override Expression<Func<Category, bool>> SearchCriteria {
            get {
                try {
                    return
                        (arg =>
                         arg.Code.ToString(Culture).ToUpper().Contains(Search.ToUpper()) || arg.Description.ToUpper().Contains(Search.ToUpper()) ||
                         arg.Name.ToUpper().Contains(Search.ToUpper()));
                } catch(FormatException) {
                    return arg => false;
                }
            }
        }
    }
}