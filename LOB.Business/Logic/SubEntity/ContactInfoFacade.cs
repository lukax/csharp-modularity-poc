#region Usings

using System.Collections.Generic;
using System.Text.RegularExpressions;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Business.Logic.Base;
using LOB.Core.Localization;
using LOB.Dao.Interface;
using LOB.Domain.Logic;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Business.Logic.SubEntity {
    public sealed class ContactInfoFacade : BaseEntityFacade<ContactInfo>, IContactInfoFacade {
        public ContactInfoFacade(IRepository repository)
            : base(repository) { ConfigureValidations(); }

        public override ContactInfo GenerateEntity() {
            var result = base.GenerateEntity();
            result.Description = "";
            result.Status = default(ContactStatus);
            result.PS = "";
            result.Emails = new List<Email>();
            result.PhoneNumbers = new List<PhoneNumber>();
            result.SpeakWith = "";
            result.WebSite = "http://";
            return result;
        }

        public void ConfigureValidations() {
            AddValidation(
                (sender, name) =>
                string.IsNullOrWhiteSpace(Entity.Description) ? new ValidationResult("Description", Strings.Notification_Field_Empty) : null);
            AddValidation(
                (sender, name) =>
                !Regex.IsMatch(Entity.WebSite,
                               @"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$")
                    ? new ValidationResult("WebSite", Strings.Notification_Field_WrongFormat)
                    : null);
            AddValidation(
                (sender, name) =>
                Entity.SpeakWith.Length > 300 ? new ValidationResult("SpeakWith", string.Format(Strings.Notification_Field_X_MaxLength, 300)) : null);
        }
    }
}