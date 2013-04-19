﻿#region Usings

using System;
using System.ComponentModel.Composition;
using LOB.Business.Interface.Logic;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Business.Logic.Base;
using LOB.Core.Localization;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.Domain.Logic;

#endregion

namespace LOB.Business.Logic {
    [Export(typeof(IEmployeeFacade))]
    public class EmployeeFacade : BaseEntityFacade<Employee>, IEmployeeFacade {
        private readonly IAddressFacade _addressFacade;
        private readonly IContactInfoFacade _contactInfoFacade;
        private readonly IPayCheckFacade _payCheckFacade;

        [ImportingConstructor]
        public EmployeeFacade(IAddressFacade addressFacade, IContactInfoFacade contactInfoFacade, IPayCheckFacade payCheckFacade,
            IRepository repository)
            : base(repository) {
            _addressFacade = addressFacade;
            _contactInfoFacade = contactInfoFacade;
            _payCheckFacade = payCheckFacade;
            ConfigureValidations();
        }

        public override Employee GenerateEntity() {
            var result = base.GenerateEntity();
            result.Address = _addressFacade.GenerateEntity();
            result.ContactInfo = _contactInfoFacade.GenerateEntity();
            result.Notes = "";
            result.BirthDate = DateTime.Now;
            result.CPF = "";
            result.FirstName = "";
            result.LastName = "";
            result.HireDate = DateTime.Now;
            result.NickName = "";
            result.Password = "";
            result.PayCheck = _payCheckFacade.GenerateEntity();
            result.RG = "";
            result.RGUF = "";
            result.Title = "";
            result.WorksIn = new Store(); //TODO
            return result;
        }

        public void ConfigureValidations() {
            AddValidation(
                (sender, name) => string.IsNullOrWhiteSpace(Entity.Title) ? new ValidationResult("Title", Strings.Notification_Field_Empty) : null);
            AddValidation(delegate {
                              if(Entity.HireDate.CompareTo(new DateTime(1990, 1, 1)) < 0) return new ValidationResult("HireDate", Strings.Notification_Field_DateTooEarly);
                              if(Entity.HireDate.CompareTo(new DateTime(2015, 1, 1)) > 0) return new ValidationResult("HireDate", Strings.Notification_Field_DateTooLate);
                              return null;
                          });
            AddValidation(delegate {
                              if(Entity.BirthDate.CompareTo(new DateTime(1900, 1, 1)) < 0) return new ValidationResult("BirthDate", Strings.Notification_Field_DateTooEarly);
                              if(Entity.BirthDate.CompareTo(new DateTime(2013, 1, 1)) > 0) return new ValidationResult("BirthDate", Strings.Notification_Field_DateTooLate);
                              return null;
                          });
        }
    }
}