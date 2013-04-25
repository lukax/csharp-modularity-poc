#region Usings

using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using LOB.Business.Contract.Logic.Base;
using LOB.Business.Contract.Logic.SubEntity;
using LOB.Business.Logic.Base;
using LOB.Core.Localization;
using LOB.Dao.Contract;
using LOB.Domain.Logic;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Business.Logic.SubEntity {
    [Export(typeof(IEmailFacade)), Export(typeof(IBaseEntityFacade<Email>))]
    public sealed class EmailFacade : BaseEntityFacade<Email>, IEmailFacade {
        [ImportingConstructor]
        public EmailFacade(IRepository repository)
            : base(repository) { ConfigureValidations(); }

        public override Email GenerateEntity() {
            var result = base.GenerateEntity();
            result.Value = "";
            return result;
        }

        public void ConfigureValidations() {
            AddValidation(delegate {
                              if(string.IsNullOrWhiteSpace(Entity.Value)) return new ValidationResult("Value", Strings.Notification_Field_Empty);
                              if(
                                  !Regex.IsMatch(Entity.Value,
                                                 @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")) return new ValidationResult("Value", Strings.Notification_Field_WrongFormat);
                              return null;
                          });
        }
    }
}