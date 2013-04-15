#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Core.Localization;
using LOB.Domain.Logic;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Business.Logic.SubEntity {
    public class ShipmentInfoInfoFacade : IShipmentInfoFacade {
        private ShipmentInfo _entity;
        public ShipmentInfo Entity {
            set {
                _entity = value;
                ConfigureValidations();
            }
        }

        public void ConfigureValidations() {
            if(_entity != null) {
                _entity.AddValidation(
                    (sender, name) => _entity.DaySchedule.Length < 1 ? new ValidationResult("DaySchedule", Strings.Notification_Field_Empty) : null);
                _entity.AddValidation(delegate {
                                          if(_entity.DeliverDate.CompareTo(new DateTime(2013, 1, 1)) < 0) return new ValidationResult("DeliverDate", Strings.Notification_Field_DateTooEarly);
                                          if(_entity.DeliverDate.CompareTo(new DateTime(2015, 1, 1)) > 0) return new ValidationResult("DeliverDate", Strings.Notification_Field_DateTooLate);
                                          return null;
                                      });
            }
        }

        public ShipmentInfo GenerateEntity() {
            return new ShipmentInfo {
                Code = 0,
                Address = null,
                Error = null,
                //Description = "",
                Status = default(ShipmentStatus),
                DaySchedule = "",
                DeliverDate = DateTime.Now,
                //Name = "",
                Products = null,
            };
        }

        public void SetEntity<T>(T entity) where T : ShipmentInfo { _entity = entity; }

        public bool CanAdd(out IEnumerable<ValidationResult> invalidFields) {
            bool result = ProcessBasicValidations(out invalidFields);
            return result;
        }

        public bool CanUpdate(out IEnumerable<ValidationResult> invalidFields) {
            bool result = ProcessBasicValidations(out invalidFields);
            return result;
        }

        public bool CanDelete(out IEnumerable<ValidationResult> invalidFields) {
            bool result = ProcessBasicValidations(out invalidFields);
            return result;
        }

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