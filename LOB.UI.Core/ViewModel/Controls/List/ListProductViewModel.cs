#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.List.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List
{
    [Export]
    public sealed class ListProductViewModel : ListBaseEntityViewModel<Product>
    {
        #region Props

        private Lazy<IQueryable<Product>> _products;
        public IList<Product> Products { get { return _products.Value.ToList(); } }

        public Product Product
        {
            get { return Entity; }
            set
            {
                if (Entity == value) return;
                Entity = value;
                OnPropertyChanged();
            }
        }

        #endregion



        [ImportingConstructor]
        public ListProductViewModel(Product product, IRepository repository)
            : base(product, repository)
        {
            _products = new Lazy<IQueryable<Product>>(Repository.GetList<Product>);
        }

        public override bool CanUpdate(object arg)
        {
            //TODO: Business logic
            return true;
        }

        public override bool CanDelete(object arg)
        {
            //TODO: Business logic
            return true;
        }

        public override void InitializeServices()
        {
        }

        public override void Refresh()
        {
        }
    }
}