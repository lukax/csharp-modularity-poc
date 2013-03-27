#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
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

        public ShipmentInfoInfoFacade(IBaseEntityFacade baseEntityFacade) {
            _baseEntityFacade = baseEntityFacade;
        }


        public void ConfigureValidations() {
            _baseEntityFacade.ConfigureValidations();
            if (_entity != null) {
                _entity.AddValidation((sender, name) => _entity.DaySchedule < 1
                                                            ? new ValidationResult("Value", Strings.Error_Field_Empty)
                                                            : null);
            }
        }

        public bool CanAdd(out IEnumerable<ValidationResult> invalidFields) {
            bool result = ProcessBasicValidations(out invalidFields);
            //TODO: Repository validations here
            return result;
        }

        public void SetEntity<T>(T entity) where T : ShipmentInfo {
            _entity = entity;
        }

        public bool CanUpdate(out IEnumerable<ValidationResult> invalidFields) {
            throw new NotImplementedException();
        }

        public bool CanDelete(out IEnumerable<ValidationResult> invalidFields) {
            throw new NotImplementedException();
        }

        void IBaseEntityFacade.SetEntity<T>(T entity) {
            ((IBaseEntityFacade) _baseEntityFacade).SetEntity(entity);
        }

        private bool ProcessBasicValidations(out IEnumerable<ValidationResult> invalidFields) {
            var fields = new List<ValidationResult>();
            fields.AddRange(_entity.GetValidations("DaySchedule"));
            invalidFields = fields;
            if (fields.Where(validationResult => validationResult != null)
                      .Count(validationResult => !string.IsNullOrEmpty(validationResult.ErrorDescription)) > 0)
                return false;
            return true;
        }
    }
}