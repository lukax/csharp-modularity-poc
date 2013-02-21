using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Input;
using LOB.Domain;
using LOB.UI.Core.Command;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;

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
            get
            {
                return Entity.Suppliers ?? new List<Supplier>();
            }
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
            get
            {
                return Entity.StockedStores ?? new List<Store>();
            }
            set
            {
                if (StockedStores == value) return;
                Entity.StockedStores = value;
                OnPropertyChanged();
            }
        }
        #endregion

        //public ICommand CancelCommand { get; set; }
        //public ICommand SaveChangesCommand { get; set; }
        public ICommand ClearEntityCommand { get; set; }

        [ImportingConstructor]
        public AlterProductViewModel()
            : base(new Product())
        {
            Entity = new Product();
            ClearEntityCommand = new DelegateCommand(ClearEntity);

        }

        private void ClearEntity(object args)
        {
            Entity = new Product();
        }

        public override void InitializeServices()
        {
            throw new System.NotImplementedException();
        }

        public override void Refresh()
        {
            throw new System.NotImplementedException();
        }
    }
}