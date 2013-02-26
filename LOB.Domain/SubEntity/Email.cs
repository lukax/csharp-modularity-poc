#region Usings

using LOB.Domain.Base;

#endregion

namespace LOB.Domain.SubEntity
{
    public class Email : BaseEntity
    {
        public virtual string Value { get; set; }

        public static implicit operator string(Email e) {
            return e.Value;
        }

        public static implicit operator Email(string value) {
            return new Email {Value = value};
        }
    }
}