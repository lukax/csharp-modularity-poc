#region Usings

using System;
using System.Collections.Generic;
using LOB.Business.Contract.Logic.Base;
using LOB.Core.Localization;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Business.Contract.Logic.SubEntity {
    public interface IAddressFacade : IBaseEntityFacade<Address> {}

    public static class AddressStatusDictionary {
        private static readonly Lazy<IDictionary<string, AddressStatus>> Lazy =
                new Lazy<IDictionary<string, AddressStatus>>(
                        () =>
                        new Dictionary<string, AddressStatus> {
                                {Strings.Common_Address_Active, AddressStatus.Active},
                                {Strings.Common_Address_Deprecated, AddressStatus.Deprecated},
                                {Strings.Common_Address_Inactive, AddressStatus.Inactive},
                        });

        public static IDictionary<string, AddressStatus> Statuses {
            get { return Lazy.Value; }
        }
    }
}