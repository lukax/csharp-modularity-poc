#region Usings

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using LOB.Core.Localization;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain.SubEntity {
    [Serializable]
    public class Person : BaseEntity, IEquatable<Person> {
        public Address Address { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public string Notes { get; set; }
        #region Implementation of IEquatable<Person>

        public bool Equals(Person other) {
            try {
                return base.Equals(other) && other.Address.Equals(Address) && other.ContactInfo.Equals(ContactInfo) && other.Notes.Equals(Notes);
            } catch(NullReferenceException ex) {
#if DEBUG
                Debug.WriteLine(ex.Message);
#endif
                return false;
            }
        }

        #endregion
    }

    [Serializable]
    public enum PersonType {
        Unknown = 0,
        Natural,
        Legal
    }

    public static class PersonExtension {
        public static IDictionary<PersonType, string> PersonTypesLocalizationsDict {
            get {
                return new Dictionary<PersonType, string> {
                    {PersonType.Natural, Strings.Common_NaturalPerson},
                    {PersonType.Legal, Strings.Common_LegalPerson}
                };
            }
        }
        public static PersonType ToPersonType(this string s) {
            if(string.IsNullOrWhiteSpace(s)) return default(PersonType);
            return PersonTypesLocalizationsDict.FirstOrDefault(x => x.Value.ToLower() == s.ToLower()).Key;
        }
        public static string ToLocalizedString(this PersonType s) { return PersonTypesLocalizationsDict[s]; }
    }
}