#region Usings

using System;
using System.Collections.Generic;
using LOB.Business.Interface.Logic.Base;
using LOB.Core.Localization;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Business.Interface.Logic.SubEntity {
    public interface IAddressFacade : IBaseEntityFacade {

        new void SetEntity<T>(T entity) where T : Address;
        Address GenerateEntity();

    }

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