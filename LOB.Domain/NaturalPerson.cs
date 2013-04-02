#region Usings

using System;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain {
    [Serializable]
    public class NaturalPerson : Person {

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }

        public string NickName { get; set; }
        public DateTime BirthDate { get; set; }
        public int CPF { get; set; }
        public int RG { get; set; }
        public string RGUF { get; set; }

    }
}