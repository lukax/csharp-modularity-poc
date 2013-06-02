#region Usings

using System;
using System.ComponentModel.Composition;
using LOB.Business.Contract.Logic;
using LOB.Business.Logic.Base;
using LOB.Dao.Contract;
using LOB.Domain;

#endregion

namespace LOB.Business.Logic {
    [Export(typeof(IOrderFacade)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class OrderFacade : BaseEntityFacade, IOrderFacade {
        [ImportingConstructor]
        public OrderFacade(IRepository repository)
                : base(repository) { }

        public Order Generate() { throw new NotImplementedException(); }
    }
}