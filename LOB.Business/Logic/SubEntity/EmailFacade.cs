#region Usings

using System.ComponentModel.Composition;
using LOB.Business.Contract.Logic.Base;
using LOB.Business.Contract.Logic.SubEntity;
using LOB.Business.Logic.Base;
using LOB.Dao.Contract;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Business.Logic.SubEntity {
    [Export(typeof(IEmailFacade)), Export(typeof(IBaseEntityFacade<Email>)), PartCreationPolicy(CreationPolicy.NonShared)]
    public sealed class EmailFacade : BaseEntityFacade, IEmailFacade {
        [ImportingConstructor]
        public EmailFacade(IRepository repository)
                : base(repository) { }

        public Email Generate() {
            var result = new Email {Value = ""};
            return result;
        }
    }
}