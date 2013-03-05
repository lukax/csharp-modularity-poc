﻿#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.Domain.SubEntity;
using LOB.UI.Core.Command;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Core.ViewModel.Controls.Alter.SubEntity;
using LOB.UI.Core.ViewModel.Controls.List;
using LOB.UI.Interface;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter
{
    [Export]
    public sealed class AlterProductViewModel : AlterBaseEntityViewModel<Product>
    {
        private IUnityContainer _container;
        private IFluentNavigator _navigator;

        public ICommand AlterCategoryCommand { get; set; }
        public ICommand ListCategoryCommand { get; set; }
        [ImportingConstructor]
        public AlterProductViewModel(Product product, IRepository repository, IUnityContainer container, IFluentNavigator navigator)
            : base(product, repository)
        {
            _container = container;
            _navigator = navigator;
            AlterCategoryCommand = new DelegateCommand(ExecuteAlterCategory);
            ListCategoryCommand = new DelegateCommand(ExecuteListCategory);
            Categories = Repository.GetList<Category>().ToList();
        }

        private void ExecuteListCategory(object o)
        {
            _navigator.Resolve(o.ToString()).Show(true);
        }

        private void ExecuteAlterCategory(object o)
        {
            if (Entity.Category != null)
                _navigator.Resolve(o.ToString(), _container.Resolve<AlterCategoryViewModel>(new ParameterOverride("category", Entity.Category))).Show(true);
            _navigator.Resolve(o.ToString()).Show(true);
            //Messenger.Default.Send<object>(_container.Resolve<AlterCategoryViewModel>());
        }

        public IList<Category> Categories { get; set; }

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
            Messenger.Default.Send<object>(_container.Resolve<ListProductViewModel>(), "QuickSearchCommand");
        }

        protected override void ClearEntity(object args)
        {
            Entity = new Product();
        }
    }
}