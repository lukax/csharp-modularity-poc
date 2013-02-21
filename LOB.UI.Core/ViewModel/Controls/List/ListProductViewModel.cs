#region Usings

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
    public class ListProductViewModel : ListBaseEntityViewModel<Product>
    {
        #region Props

        private IList<Product> _products;

        public IList<Product> Products
        {
            get { return _products; }
            set
            {
                if (Products == value) return;
                _products = value;
                OnPropertyChanged();
            }
        }

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
            Products = Repository.GetList<Product>().ToList();
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