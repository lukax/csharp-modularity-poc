#region Usings

using LOB.Business.Interface.Logic;
using LOB.Business.Logic.Base;
using LOB.Dao.Interface;
using LOB.Domain;

#endregion

namespace LOB.Business.Logic {
    public class SaleFacade : BaseEntityFacade<Sale>, ISaleFacade {
        public SaleFacade(IRepository repository)
            : base(repository) { }
    }
}