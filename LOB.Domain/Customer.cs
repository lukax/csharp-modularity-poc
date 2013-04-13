#region Usings

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using LOB.Core.Localization;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain {
    [Serializable]
    public class Customer : BaseEntity, IEquatable<Customer> {

        public Person Person { get; set; }
        public PersonType PersonType { get; set; }
        public IList<Store> CustomerOf { get; set; }
        public CustomerStatus Status { get; set; }
        public IList<Sale> BoughtHistory { get; set; }
        #region Implementation of IEquatable<Customer>

        public bool Equals(Customer other) {
            try {
                return base.Equals(other) && other.Person.Equals(other) && other.PersonType.Equals(PersonType) &&
                       other.CustomerOf.SequenceEqual(CustomerOf) && other.Status.Equals(Status) && other.BoughtHistory.SequenceEqual(BoughtHistory);
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
    public enum CustomerStatus {

        New,
        Active,
        Inactive

    }

    public static class CustomerExtensions {

        public static IDictionary<CustomerStatus, string> CustomerStatusLocalizationsDict {
            get {
                return new Dictionary<CustomerStatus, string> {
                    {CustomerStatus.New, Strings.Common_New},
                    {CustomerStatus.Active, Strings.Common_Active},
                    {CustomerStatus.Inactive, Strings.Common_Inactive}
                };
            }
        }
        public static CustomerStatus ToCustomerStatus(this string s) { return CustomerStatusLocalizationsDict.FirstOrDefault(x => x.Value.ToLower() == s.ToLower()).Key; }
        public static string ToLocalizedString(this CustomerStatus s) { return CustomerStatusLocalizationsDict[s]; }

    }
}