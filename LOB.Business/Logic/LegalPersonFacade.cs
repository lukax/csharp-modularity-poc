#region Usings

using System.ComponentModel.Composition;
using LOB.Business.Contract.Logic;
using LOB.Business.Contract.Logic.Base;
using LOB.Business.Contract.Logic.SubEntity;
using LOB.Business.Logic.Base;
using LOB.Dao.Contract;
using LOB.Domain.Base;

#endregion

namespace LOB.Business.Logic {
    [Export(typeof(ILegalPersonFacade)), Export(typeof(IBaseEntityFacade<LegalPerson>)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class LegalPersonFacade : BaseEntityFacade, ILegalPersonFacade {
        private readonly IAddressFacade _addressFacade;

        [ImportingConstructor]
        public LegalPersonFacade(IAddressFacade addressFacade, IRepository repository)
                : base(repository) { _addressFacade = addressFacade; }

        public LegalPerson Generate() {
            LegalPerson result = new LocalLegalPerson();
            result.Address = _addressFacade.Generate();
            result.CNAEFiscal = "";
            result.CNPJ = "";
            result.CorporateName = "";
            result.InscEstadual = "";
            result.InscMunicipal = "";
            result.TradingName = "";
            return result;
        }

        private class LocalLegalPerson : LegalPerson {}
    }
}