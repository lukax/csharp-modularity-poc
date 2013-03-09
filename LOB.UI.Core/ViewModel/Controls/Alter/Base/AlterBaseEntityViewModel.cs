#region Usings

using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.ViewModel.Controls.Alter.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.Base
{
    [InheritedExport]
    public abstract class AlterBaseEntityViewModel<T> : BaseViewModel, IAlterBaseEntityViewModel<T> where T : BaseEntity
    {
        [ImportingConstructor]
        public AlterBaseEntityViewModel(T entity, IRepository repository)
        {
            Repository = repository;
            Entity = entity;
            SaveChangesCommand = new DelegateCommand(SaveChanges, CanSaveChanges);
            CancelCommand = new DelegateCommand(Cancel, CanCancel);
            QuickSearchCommand = new DelegateCommand(QuickSearch);
            ClearEntityCommand = new DelegateCommand(ClearEntity);
        }

        protected IRepository Repository { get; set; }

        public T Entity { get; set; }

        public DelegateCommand ClearEntityCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand QuickSearchCommand { get; set; }
        public int? CancelIndex { get; set; }
        public ICommand SaveChangesCommand { get; set; }

        public override void InitializeServices()
        {
        }

        public override void Refresh()
        {
        }

        protected virtual bool CanSaveChanges(object arg)
        {
            return Entity != null;
        }

        protected virtual bool CanCancel(object arg)
        {
            return Entity != null;
        }

        protected virtual void SaveChanges(object arg)
        {
            using (Repository.Uow.BeginTransaction())
            {
                Debug.Write("Saving changes...");
                Entity = Repository.SaveOrUpdate(Entity);
            }
            Cancel(arg);
        }

        protected virtual void Cancel(object arg)
        {
            Messenger.Default.Send(CancelIndex, "Cancel");
        }

        protected abstract void QuickSearch(object arg);

        protected abstract void ClearEntity(object arg);
    }
}