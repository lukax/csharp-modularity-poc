#region Usings

using System.ComponentModel.Composition;
using LOB.Business.Contract.Logic;
using LOB.Business.Logic.Base;
using LOB.Dao.Contract;
using LOB.Domain;

#endregion

namespace LOB.Business.Logic {
    [Export(typeof(ISaleFacade)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class SaleFacade : BaseEntityFacade<Sale>, ISaleFacade {
        [ImportingConstructor]
        public SaleFacade(IRepository repository)
            : base(repository) { }
    }
}