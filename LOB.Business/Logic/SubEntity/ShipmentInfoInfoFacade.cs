#region Usings

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using LOB.Business.Interface.Logic.Base;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Core.Localization;
using LOB.Domain.Logic;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Business.Logic.SubEntity {
    public class ShipmentInfoInfoFacade : IShipmentInfoFacade {

        private readonly IBaseEntityFacade _baseEntityFacade;
        private ShipmentInfo _entity;
        private CultureInfo Culture {
            get { return Thread.CurrentThread.CurrentCulture; }
        }
        public ShipmentInfoInfoFacade(IBaseEntityFacade baseEntityFacade) { _baseEntityFacade = baseEntityFacade; }

        public void ConfigureValidations() {
            _baseEntityFacade.ConfigureValidations();
            if(_entity != null) {
                _entity.AddValidation(
                    (sender, name) => _entity.DaySchedule.Length < 1 ? new ValidationResult("DaySchedule", Strings.Error_Field_Empty) : null);
                _entity.AddValidation(
                    (sender, name) =>
                    _entity.DeliverDate.CompareTo(new DateTime(2013, 1, 1)) < 0
                        ? new ValidationResult("DeliverDate", Strings.Error_Field_DateTooEarly)
                        : null);
                _entity.AddValidation(
                    (sender, name) =>
                    _entity.DeliverDate.CompareTo(new DateTime(2015, 1, 1)) > 0
                        ? new ValidationResult("DeliverDate", Strings.Error_Field_DateTooLate)
                        : null);
            }
        }

        public ShipmentInfo GenerateEntity() {
            return new ShipmentInfo {
                Code = 0,
                Address = null,
                Error = null,
                Description = "",
                Status = default(ShipmentStatus),
                DaySchedule = "",
                DeliverDate = DateTime.Now,
                Name = "",
                Products = null,
            };
        }

        public void SetEntity<T>(T entity) where T : ShipmentInfo { _entity = entity; }

        public bool CanAdd(out IEnumerable<ValidationResult> invalidFields) {
            bool result = ProcessBasicValidations(out invalidFields);
            //TODO: Repository validations here
            return result;
        }

        public bool CanUpdate(out IEnumerable<ValidationResult> invalidFields) {
            bool result = ProcessBasicValidations(out invalidFields);
            //TODO: Repository validations here
            return result;
        }

        public bool CanDelete(out IEnumerable<ValidationResult> invalidFields) {
            bool result = ProcessBasicValidations(out invalidFields);
            //TODO: Repository validations here
            return result;
        }

        void IBaseEntityFacade.SetEntity<T>(T entity) { (_baseEntityFacade).SetEntity(entity); }

        private bool ProcessBasicValidations(out IEnumerable<ValidationResult> invalidFields) {
            var fields = new List<ValidationResult>();
            fields.AddRange(_entity.GetValidations("DaySchedule"));
            fields.AddRange(_entity.GetValidations("DeliverDate"));
            invalidFields = fields;
            if(
                fields.Where(validationResult => validationResult != null)
                      .Count(validationResult => !string.IsNullOrEmpty(validationResult.ErrorDescription)) > 0) return false;
            return true;
        }

    }
}