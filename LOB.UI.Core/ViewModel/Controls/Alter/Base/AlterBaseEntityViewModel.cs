#region Usings

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.UI.Core.Command;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using LOB.UI.Interface;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.Base
{
    [InheritedExport]
    public abstract class AlterBaseEntityViewModel<T> : BaseViewModel where T : BaseEntity
    {
        public T Entity { get; set; }
        
        [ImportingConstructor]
        public AlterBaseEntityViewModel(T entity, IRepository repository)
        {
            Entity = entity;
            Repository = repository;
            SaveChangesCommand = new DelegateCommand(SaveChanges, CanSaveChanges);
            CancelCommand = new DelegateCommand(Cancel, CanCancel);
            QuickSearchCommand = new DelegateCommand(QuickSearch);
            ClearEntityCommand = new DelegateCommand(ClearEntity);
        }

        public DelegateCommand ClearEntityCommand { get; set; }
        protected IRepository Repository { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand QuickSearchCommand { get; set; }
        public int? CancelIndex { get; set; }
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
            Debug.Write("Saving changes...");
            Entity = Repository.SaveOrUpdate(Entity);
            Cancel(arg);
        }

        protected virtual void Cancel(object arg)
        {
            Messenger.Default.Send(CancelIndex, "Cancel");
        }

        protected abstract void QuickSearch(object arg);

        protected abstract void ClearEntity(object arg);

        public override void InitializeServices()
        {
        }

        public override void Refresh()
        {
        }
    }
}