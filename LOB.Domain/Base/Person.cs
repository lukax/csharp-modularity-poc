#region Usings

using System;
using System.Collections.Generic;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Domain.Base
{
    [Serializable]
    public abstract class Person : BaseEntity
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string NickName { get; set; }
        public virtual DateTime BirthDate { get; set; }
        public virtual IList<Address> Address { get; set; }
        public virtual ContactInfo ContactInfo { get; set; }
        public virtual string Notes { get; set; }
    }
}