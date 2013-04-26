#region Usings

using System;
using System.Diagnostics;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Domain {
    [Serializable]
    public class NaturalPerson : Person, IEquatable<NaturalPerson> {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }

        public string NickName { get; set; }
        public DateTime BirthDate { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string RGUF { get; set; }
        #region Implementation of IEquatable<NaturalPerson>

        public bool Equals(NaturalPerson other) {
            try {
                return base.Equals(other) && other.FirstName.Equals(FirstName) && other.LastName.Equals(LastName) && other.NickName.Equals(NickName) &&
                       other.BirthDate.Equals(BirthDate) && other.CPF.Equals(CPF) && other.RG.Equals(RG) && other.RGUF.Equals(RGUF);
            } catch(NullReferenceException ex) {
#if DEBUG
                Debug.WriteLine(ex.Message);
#endif
                return false;
            }
        }

        #endregion
    }
}