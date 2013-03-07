#region Usings

using System;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain
{
    [Serializable]
    public class NaturalPerson : Person
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }

        public virtual string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }

        public virtual string NickName { get; set; }
        public virtual DateTime BirthDate { get; set; }
        public virtual int Cpf { get; set; }
        public virtual int Rg { get; set; }
        public virtual string RgUf { get; set; }
    }
}