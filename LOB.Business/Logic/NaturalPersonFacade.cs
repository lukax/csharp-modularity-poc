#region Usings

using System;
using System.ComponentModel.Composition;
using LOB.Business.Contract.Logic;
using LOB.Business.Contract.Logic.Base;
using LOB.Business.Contract.Logic.SubEntity;
using LOB.Business.Logic.Base;
using LOB.Dao.Contract;
using LOB.Domain.Base;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Business.Logic {
    [Export(typeof(INaturalPersonFacade)), Export(typeof(IBaseEntityFacade<NaturalPerson>)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class NaturalPersonFacade : BaseEntityFacade, INaturalPersonFacade {
        private readonly IAddressFacade _addressFacade;

        [ImportingConstructor]
        public NaturalPersonFacade(IAddressFacade addressFacade, IRepository repository)
                : base(repository) { _addressFacade = addressFacade; }

        public NaturalPerson Generate() {
            NaturalPerson result = new LocalNaturalPerson {
                    FirstName = "",
                    LastName = "",
                    NickName = "",
                    BirthDate = DateTime.Now,
                    CPF = "",
                    RG = "",
                    RGUF = default(UF),
                    Address = _addressFacade.Generate()
            };
            return result;
        }

        private class LocalNaturalPerson : NaturalPerson {}
    }
}