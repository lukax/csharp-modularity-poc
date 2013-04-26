#region Usings

using System.ComponentModel.Composition;
using LOB.Domain.SubEntity;
using LOB.UI.Contract.ViewModel.Controls.Alter.Base;
using LOB.UI.Contract.ViewModel.Controls.Alter.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity {
    [Export(typeof(IAlterPersonViewModel)), Export(typeof(IAlterBaseEntityViewModel<Person>)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class AlterPersonViewModel : AlterBaseEntityViewModel<Person>, IAlterPersonViewModel {
        //protected override bool CanSaveChanges(object arg)
        //{
        //    if (ReferenceEquals(Entity, null)) return false;
        //    if (Info.ViewState == ViewState.Add)
        //        return base.CanSaveChanges(arg) & _alterAddressViewModel.SaveChangesCommand.CanExecute(arg) &&
        //               _alterContactInfoViewModel.SaveChangesCommand.CanExecute(arg);
        //    if (Info.ViewState == ViewState.UpdateExecute)
        //        return base.CanSaveChanges(arg) & _alterContactInfoViewModel.SaveChangesCommand.CanExecute(arg) &&
        //               _alterContactInfoViewModel.SaveChangesCommand.CanExecute(arg);
        //    return false;
        //}
    }
}