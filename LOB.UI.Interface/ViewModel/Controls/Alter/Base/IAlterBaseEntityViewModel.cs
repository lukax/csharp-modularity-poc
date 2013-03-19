#region Usings

using System.Windows.Input;
using LOB.Domain.Base;
using LOB.UI.Interface.Command;

#endregion

namespace LOB.UI.Interface.ViewModel.Controls.Alter.Base
{
    public interface IAlterBaseEntityViewModel<T> : IBaseViewModel where T : BaseEntity
    {
        T Entity { get; set; }
        DelegateCommand ClearEntityCommand { get; set; }
        ICommand CancelCommand { get; set; }
        ICommand QuickSearchCommand { get; set; }
        int? CancelIndex { get; set; }
        ICommand SaveChangesCommand { get; set; }
    }
}