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

namespace LOB.UI.Core.ViewModel.Controls.Alter.Base {
    public abstract class AlterBaseEntityViewModel<T> : BaseViewModel, IAlterBaseEntityViewModel where T : BaseEntity {

        [InjectionConstructor] public AlterBaseEntityViewModel(T entity, IRepository repository) {
            this.Repository = repository;
            this.Entity = entity;
            this.SaveChangesCommand = new DelegateCommand(this.SaveChanges, this.CanSaveChanges);
            this.DiscardChangesCommand = new DelegateCommand(this.Cancel, this.CanCancel);
            this.QuickSearchCommand = new DelegateCommand(this.QuickSearch);
            this.ClearEntityCommand = new DelegateCommand(this.ClearEntity);
        }

        protected IRepository Repository { get; set; }

        public T Entity { get; set; }

        public DelegateCommand ClearEntityCommand { get; set; }
        public ICommand DiscardChangesCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand QuickSearchCommand { get; set; }
        public int Index { get; set; }
        public ICommand SaveChangesCommand { get; set; }

        protected virtual bool CanSaveChanges(object arg) {
            return this.Entity != null;
        }

        protected virtual bool CanCancel(object arg) {
            return this.Entity != null;
        }

        protected virtual void SaveChanges(object arg) {
            using(this.Repository.Uow.BeginTransaction()) {
                Debug.Write("Saving changes...");
                this.Entity = this.Repository.SaveOrUpdate(this.Entity);
            }
            this.Cancel(arg);
        }

        protected virtual void Cancel(object arg) {}

        protected abstract void QuickSearch(object arg);
        protected abstract void ClearEntity(object arg);

    }
}