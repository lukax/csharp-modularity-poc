#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using LOB.Business.Contract.Logic;
using LOB.Business.Contract.Logic.Base;
using LOB.Business.Contract.Logic.SubEntity;
using LOB.Business.Logic.Base;
using LOB.Dao.Contract;
using LOB.Domain;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Business.Logic.SubEntity {
    [Export(typeof(IShipmentFacade)), Export(typeof(IBaseEntityFacade<Shipment>)), PartCreationPolicy(CreationPolicy.NonShared)]
    public sealed class ShipmentFacade : BaseEntityFacade, IShipmentFacade {
        private readonly IAddressFacade _addressFacade;

        [ImportingConstructor]
        public ShipmentFacade(IAddressFacade addressFacade, IRepository repository)
                : base(repository) { _addressFacade = addressFacade; }

        public Shipment Generate() {
            var result = new Shipment {
                    Status = default(ShipmentStatus),
                    ScheduleDate = DateTime.Now,
                    DeliverDate = DateTime.Now,
                    Products = new List<Product>()
            };
            return result;
        }
    }
}