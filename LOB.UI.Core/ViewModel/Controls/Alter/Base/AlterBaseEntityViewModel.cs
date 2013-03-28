#region Usings

using System.Diagnostics;
using System.Windows.Input;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.ViewModel.Controls.Alter.Base;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.Base
{
    public abstract class AlterBaseEntityViewModel<T> : BaseViewModel, IAlterBaseEntityViewModel where T : BaseEntity
    {
        [InjectionConstructor]
        public AlterBaseEntityViewModel(T entity, IRepository repository)
        {
            Repository = repository;
            Entity = entity;
            SaveChangesCommand = new DelegateCommand(SaveChanges, CanSaveChanges);
            DiscardChangesCommand = new DelegateCommand(Cancel, CanCancel);
            QuickSearchCommand = new DelegateCommand(QuickSearch);
            ClearEntityCommand = new DelegateCommand(ClearEntity);
        }

        protected IRepository Repository { get; set; }

        public T Entity { get; set; }

        public DelegateCommand ClearEntityCommand { get; set; }
        public ICommand DiscardChangesCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand QuickSearchCommand { get; set; }
        public int Index { get; set; }
        public ICommand SaveChangesCommand { get; set; }

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
        }

        protected abstract void QuickSearch(object arg);
        protected abstract void ClearEntity(object arg);
    }
}