#region Usings

using System.ComponentModel.Composition;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Dao.Interface;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity {
    [Export(typeof(IAlterPhoneNumberViewModel))]
    public sealed class AlterPhoneNumberViewModel : AlterBaseEntityViewModel<PhoneNumber>, IAlterPhoneNumberViewModel {
        [ImportingConstructor]
        public AlterPhoneNumberViewModel(IPhoneNumberFacade phoneNumberFacade, IRepository repository, IEventAggregator eventAggregator,
            ILoggerFacade logger)
            : base(phoneNumberFacade, repository, eventAggregator, logger) { }

        public override void InitializeServices() { base.InitializeServices(); }
    }
}