#region Usings

using System.ComponentModel.Composition;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity {
    [Export(typeof(IAlterEmailViewModel))]
    public sealed class AlterEmailViewModel : AlterBaseEntityViewModel<Email>, IAlterEmailViewModel {
        [ImportingConstructor]
        public AlterEmailViewModel(IEmailFacade emailFacade)
            : base(emailFacade) { }
    }
}