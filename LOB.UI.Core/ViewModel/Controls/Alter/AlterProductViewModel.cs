#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.Domain.SubEntity;
using LOB.UI.Core.Command;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter
{
    [Export]
    public class AlterProductViewModel : AlterBaseEntityViewModel<Product>
    {
        #region Props

        public string Name
        {
            get { return Entity.Name; }
            set
            {
                if (Entity.Name != value)
                {
                    if (Entity.Name == value) return;
                    Entity.Name = value;
                    OnPropertyChanged();
                }
            }
        }

        public int UnitsInStock
        {
            get { return Entity.UnitsInStock; }
            set
            {
                if (Entity.UnitsInStock == value) return;
                Entity.UnitsInStock = value;
                OnPropertyChanged();
            }
        }

        public string QuantityPerUnit
        {
            get { return Entity.QuantityPerUnit; }
            set
            {
                if (Entity.QuantityPerUnit == value) return;
                Entity.QuantityPerUnit = value;
                OnPropertyChanged();
            }
        }

        public double UnitSalePrice
        {
            get { return Entity.UnitSalePrice; }
            set
            {
                if (Entity.UnitSalePrice == value) return;
                Entity.UnitSalePrice = value;
                OnPropertyChanged();
            }
        }

        public IList<Supplier> Suppliers
        {
            get { return Entity.Suppliers ?? new List<Supplier>(); }
            set
            {
                if (Entity.Suppliers == value) return;
                Entity.Suppliers = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get { return Entity.Description; }
            set
            {
                if (Entity.Description == value) return;
                Entity.Description = value;
                OnPropertyChanged();
            }
        }

        public IList<Store> StockedStores
        {
            get { return Entity.StockedStores ?? new List<Store>(); }
            set
            {
                if (Entity.StockedStores == value) return;
                Entity.StockedStores = value;
                OnPropertyChanged();
            }
        }

        public Category Category
        {
            get { return Entity.Category; }
            set
            {
                if (Entity.Category == value) return;
                Entity.Category = value;
                OnPropertyChanged();
            }
        }

        private Lazy<IList<Category>> _categories; 
        public IList<Category> Categories
        {
            get { return _categories.Value; }
        }
        
        public ProductStatus Status
        {
            get { return Entity.Status; }
            set
            {
                if (Entity.Status == value) return;
                Entity.Status = value;
                OnPropertyChanged();
            }
        }
        #endregion
        public ICommand ClearEntityCommand { get; set; }

        [ImportingConstructor]
        public AlterProductViewModel(Product product, IRepository repository)
            : base(product, repository)
        {
            _categories = new Lazy<IList<Category>>(Repository.GetList<Category>().ToList);
            ClearEntityCommand = new DelegateCommand(ClearEntity);
        }

        public override void SaveChanges(object arg)
        {
            //base.SaveChanges(arg);
            using (Repository.Uow)
            {
                Repository.Uow.BeginTransaction();
                Repository.SaveOrUpdate(BuildProduct());
                Repository.Uow.CommitTransaction();
            }
        }

        public override bool CanSaveChanges(object arg)
        {
            //TODO: Business logic
            return true;
        }

        public override bool CanCancel(object arg)
        {
            //TODO: Business logic
            return true;
        }

        private void ClearEntity(object args)
        {
            Entity = new Product();
        }

        public override void InitializeServices()
        {
        }

        public override void Refresh()
        {
        }

        private Product BuildProduct()
        {
            return new Product()
            {
                Name = Name,
                UnitSalePrice = UnitSalePrice,
                StockedStores = StockedStores,
                Description = Description,
                QuantityPerUnit = QuantityPerUnit,
                UnitsInStock = UnitsInStock,
                Suppliers = Suppliers,
                Category = Category, 
                Status = Status
            };
        }
    }
}