#region Usings

using System.ComponentModel.Composition;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Base;
using System.Collections.Generic;
using System;
using LOB.Domain.Base;
using LOB.Domain.SubEntity;

#endregion

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
                Entity.UnitSalePrice = value;
                OnPropertyChanged();
            }
        }
        public IList<Supplier> Supplier
        {
            get { return Entity.Supplier; }
            set
            {
                if (Supplier != value)
                {
                    Entity.Supplier = value;
                    OnPropertyChanged();
                }
            }
        }
        public int UnitsInStock
        {
            get { return Entity.UnitsInStock; }
            set
            {
                if (UnitsInStock != value)
                {
                    Entity.UnitsInStock = value;
                    OnPropertyChanged();
                }
            }
        }
        public string QuantityPerUnit
        {
            get { return Entity.QuantityPerUnity; }
            set
            {
                if (QuantityPerUnit != value)
                {
                    Entity.QuantityPerUnity = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Description
        {
            get { return Entity.Description; }
            set
            {
                if (Description != value)
                {
                    Description = value;
                    OnPropertyChanged();
                }
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
    public class AlterEmployeeViewModel : AlterEntityViewModel<Employee>
    {
        public string Title
        {
            get { return Entity.Title; }
            set { Entity.Title = value; }
        }
        public DateTime HireDate
        {
            get { return Entity.HireDate; }
            set { Entity.HireDate = value; }
        }
        
        public AlterEmployeeViewModel()
            : base(new Employee())
        {
            Entity = new Employee();
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


    public class AlterPersonViewModel : AlterEntityViewModel<Person>
    {

        public string FirstName
        {
            get { return Entity.FirstName; }
            set { Entity.FirstName = value; }
        }
        public string LastName
        {
            get { return Entity.LastName; }
            set { Entity.LastName = value; }
        }
        public string NickName
        {
            get { return Entity.NickName; }
            set { Entity.NickName = value; }
        }
        public DateTime BirthDate
        {
            get { return Entity.BirthDate; }
            set { Entity.BirthDate = value; }
        }
        public string Notes
        {
            get { return Entity.Notes; }
            set { Entity.Notes = value; }
        }

        public AlterPersonViewModel(Person entity)
            : base(entity)
        {

        }
    }
    public class AlterNaturalPersonViewModel : AlterPersonViewModel
    {
        protected NaturalPerson Entity { get; set; }

        public int Rg
        {
            get { return Entity.Rg; }
            set { Entity.Rg = value; }
        }
        public int Cpf
        {
            get { return Entity.Cpf; }
            set { Entity.Cpf = value; }
        }

        public AlterNaturalPersonViewModel(NaturalPerson entity)
            : base(entity)
        {
            Entity = entity;
        }
    }
    public class AlterLegalPersonViewModel : AlterPersonViewModel
    {
        protected LegalPerson Entity { get; set; }

        public int Ie
        {
            get { return Entity.Ie; }
            set { Entity.Ie = value; }
        }
        public int Cnpj
        {
            get { return Entity.Cnpj; }
            set { Entity.Cnpj = value; }
        }

        public AlterLegalPersonViewModel(LegalPerson entity)
            : base(entity)
        {
            Entity = entity;
        }
    }

    public class AlterPayCheckViewModel : AlterEntityViewModel<PayCheck>
    {
        public AlterPayCheckViewModel(PayCheck Entity)
            :base(Entity)
        {

        }
    }
    public class AlterContactInfoViewModel : AlterEntityViewModel<ContactInfo>  
    {
        public AlterContactInfoViewModel(ContactInfo Entity)
            :base(Entity)
        {

        }
    }
    public class AlterAddressViewModel : AlterEntityViewModel<Address>
    {
        public AlterAddressViewModel(Address Entity)
            :base(Entity)
        {

        }
    }

    public class AlterClientViewModel
    {
        public AlterClientViewModel(AlterEntityViewModel<Person> vm)
        {

        }
    }




}