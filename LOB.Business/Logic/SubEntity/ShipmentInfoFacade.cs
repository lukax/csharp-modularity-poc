#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Business.Logic.Base;
using LOB.Core.Localization;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.Domain.Logic;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Business.Logic.SubEntity {
    [Export(typeof(IShipmentInfoFacade))]
    public sealed class ShipmentInfoFacade : BaseEntityFacade<ShipmentInfo>, IShipmentInfoFacade {
        private readonly IAddressFacade _addressFacade;

        [ImportingConstructor]
        public ShipmentInfoFacade(IAddressFacade addressFacade, IRepository repository)
            : base(repository) {
            _addressFacade = addressFacade;
            ConfigureValidations();
        }

        public override ShipmentInfo GenerateEntity() {
            var result = base.GenerateEntity();
            result.Address = _addressFacade.GenerateEntity();
            result.Status = default(ShipmentStatus);
            result.DaySchedule = "";
            result.DeliverDate = DateTime.Now;
            result.Products = new List<Product>();
            return result;
        }

        public void ConfigureValidations() {
            AddValidation(
                (sender, name) => Entity.DaySchedule.Length < 1 ? new ValidationResult("DaySchedule", Strings.Notification_Field_Empty) : null);
            AddValidation(delegate {
                              if(Entity.DeliverDate.CompareTo(new DateTime(2013, 1, 1)) < 0) return new ValidationResult("DeliverDate", Strings.Notification_Field_DateTooEarly);
                              if(Entity.DeliverDate.CompareTo(new DateTime(2015, 1, 1)) > 0) return new ValidationResult("DeliverDate", Strings.Notification_Field_DateTooLate);
                              return null;
                          });
        }
    }
}