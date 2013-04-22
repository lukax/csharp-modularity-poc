#region Usings

using System;
using System.ComponentModel.Composition;
using System.Linq.Expressions;
using LOB.Dao.Interface;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using LOB.UI.Interface.ViewModel.Controls.List.SubEntity;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List.SubEntity {
    [Export(typeof(IListCategoryViewModel))]
    public sealed class ListCategoryViewModel : ListBaseEntityViewModel<Category>, IListCategoryViewModel {
        [ImportingConstructor]
        public ListCategoryViewModel(IRepository repository, IEventAggregator eventAggregator)
            : base() { }

        public override void InitializeServices() { base.InitializeServices(); }

        public new Expression<Func<Category, bool>> SearchCriteria {
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