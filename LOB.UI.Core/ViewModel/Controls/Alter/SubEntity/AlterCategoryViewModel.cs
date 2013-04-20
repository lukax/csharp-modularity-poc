#region Usings

using System.ComponentModel.Composition;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Dao.Interface;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Category = LOB.Domain.SubEntity.Category;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity {
    [Export(typeof(IAlterCategoryViewModel))]
    public sealed class AlterCategoryViewModel : AlterBaseEntityViewModel<Category>, IAlterCategoryViewModel {
        [ImportingConstructor]
        public AlterCategoryViewModel(IRepository repository, ICategoryFacade categoryFacade, IEventAggregator eventAggregator, ILoggerFacade logger)
            : base(categoryFacade, repository, eventAggregator, logger) { }

        public override void InitializeServices() {
            //if(Equals(ViewModelState, default(ViewModelState))) ViewModelState = _defaultModelState;
            //base.InitializeServices();
        }
    }
}