#region Usings

using System.Windows.Input;
using LOB.Domain.Base;

#endregion

namespace LOB.UI.Contract.ViewModel.Controls.Alter.Base {
    public interface IAlterBaseEntityViewModel : IBaseViewModel {
        ICommand SaveChangesCommand { get; }
        ICommand DiscardChangesCommand { get; }
        ICommand QuickSearchCommand { get; }
        ICommand CloseCommand { get; }
    }

    public interface IAlterBaseEntityViewModel<out TEntity> : IAlterBaseEntityViewModel where TEntity : BaseEntity {
        TEntity Entity { get; }
    }
}