#region Usings

using System.ComponentModel.Composition;
using LOB.Business.Interface.Logic;
using LOB.Business.Logic.Base;
using LOB.Dao.Interface;
using LOB.Domain;

#endregion

namespace LOB.Business.Logic {
    [Export(typeof(ISaleFacade))]
    public class SaleFacade : BaseEntityFacade<Sale>, ISaleFacade {
        [ImportingConstructor]
        public SaleFacade(IRepository repository)
            : base(repository) { }
    }
}