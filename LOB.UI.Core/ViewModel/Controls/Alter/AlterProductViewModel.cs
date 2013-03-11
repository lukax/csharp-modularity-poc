#region Usings

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Core.ViewModel.Controls.Alter.SubEntity;
using LOB.UI.Core.ViewModel.Controls.List;
using LOB.UI.Interface;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.ViewModel.Controls.Alter;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter
{
    //[Export(typeof(IAlterProductViewModel))]
    public sealed class AlterProductViewModel : AlterBaseEntityViewModel<Product>, IAlterProductViewModel
    {
        private IList<Category> _categories;
        private IUnityContainer _container;
        private IFluentNavigator _navigator;
        private ICommandService commandService = CommandService.Default;

        [ImportingConstructor]
        public AlterProductViewModel(Product entity, IRepository repository, IUnityContainer container,
                                     IFluentNavigator navigator)
            : base(entity, repository)
        {
            _container = container;
            _navigator = navigator;
            AlterCategoryCommand = new DelegateCommand(ExecuteAlterCategory);
            ListCategoryCommand = new DelegateCommand(ExecuteListCategory);
            UpdateCategoryList();
        }

        public ICommand AlterCategoryCommand { get; set; }
        public ICommand ListCategoryCommand { get; set; }

        public IList<Category> Categories
        {
            get { return _categories; }
            set
            {
                if (value == null) return;
                if (value.Equals(_categories)) return;
                _categories = value;
                OnPropertyChanged();
                if (Entity.Category == null) Entity.Category = value.FirstOrDefault();
            }
        }

        private async void UpdateCategoryList(int delay = 2000)
        {
            while (true)
            {
                await Task.Delay(2000);
                Categories = Repository.GetList<Category>().ToList();
            }
        }

        private void ExecuteListCategory(object o)
        {
            _navigator.ResolveView(o.ToString()).Show(true);
        }

        private void ExecuteAlterCategory(object o)
        {
            if (Entity.Category != null)
                _navigator.ResolveView(o.ToString())
                          .SetViewModel(
                              _container.Resolve<AlterCategoryViewModel>(new ParameterOverride("category",
                                                                                               Entity.Category)))
                          .Show(true);
            _navigator.ResolveView(o.ToString()).Show(true);
            //Messenger.Default.Send<object>(_container.ResolveView<AlterCategoryViewModel>());
        }

        protected override void SaveChanges(object arg)
        {
            using (Repository.Uow)
            {
                Repository.Uow.BeginTransaction();
                Entity = Repository.SaveOrUpdate(Entity);
                Repository.Uow.CommitTransaction();
            }

            Messenger.Default.Send("SaveChangesCommand");
        }

        protected override bool CanSaveChanges(object arg)
        {
            //TODO: Business logic
            return true;
        }

        protected override bool CanCancel(object arg)
        {
            //TODO: Business logic
            return true;
        }

        protected override void QuickSearch(object arg)
        {
            commandService["QuickSearch"].Execute(_container.Resolve<ListProductViewModel>());
            //Messenger.Default.Send<object>(_container.Resolve<ListProductViewModel>(), "QuickSearchCommand");
        }

        protected override void ClearEntity(object args)
        {
            Entity = new Product();
        }
    }
}