using System.Collections.Generic;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Base;

namespace LOB.UI.Core.ViewModel.Controls
{
    public class AlterProductViewModel : AlterEntityViewModel<Product>
    {
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
            get { return Entity.Suppliers; }
            set
            {
                if (Suppliers == value) return;
                Entity.Suppliers = value;
                OnPropertyChanged();
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
        
        public AlterProductViewModel()
            : base(new Product())
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