#region Usings

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Input;
using LOB.Dao.Interface;
using LOB.Domain;
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
                if (Name != value)
                {
                    if (Name == value) return;
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
                if (UnitsInStock == value) return;
                Entity.UnitsInStock = value;
                OnPropertyChanged();
            }
        }

        public string QuantityPerUnit
        {
            get { return Entity.QuantityPerUnity; }
            set
            {
                if (QuantityPerUnit == value) return;
                Entity.QuantityPerUnity = value;
                OnPropertyChanged();
            }
        }

        public double UnitSalePrice
        {
            get { return Entity.UnitSalePrice; }
            set
            {
                if (UnitSalePrice == value) return;
                Entity.UnitSalePrice = value;
                OnPropertyChanged();
            }
        }

        public IList<Supplier> Suppliers
        {
            get { return Entity.Suppliers ?? new List<Supplier>(); }
            set
            {
                if (Suppliers == value) return;
                Entity.Suppliers = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get { return Entity.Description; }
            set
            {
                if (Description == value) return;
                Description = value;
                OnPropertyChanged();
            }
        }

        public IList<Store> StockedStores
        {
            get { return Entity.StockedStores ?? new List<Store>(); }
            set
            {
                if (StockedStores == value) return;
                Entity.StockedStores = value;
                OnPropertyChanged();
            }
        }

        #endregion

        [ImportingConstructor]
        public AlterProductViewModel(Product product, IRepository repository)
            : base(product, repository)
        {
            ClearEntityCommand = new DelegateCommand(ClearEntity);
        }

        public ICommand ClearEntityCommand { get; set; }

        private void ClearEntity(object args)
        {
            Entity = new Product();
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

        public override void InitializeServices()
        {
        }

        public override void Refresh()
        {
        }
    }
}