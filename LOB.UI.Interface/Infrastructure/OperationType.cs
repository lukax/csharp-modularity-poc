#region Usings

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace LOB.UI.Interface.Infrastructure
{
    public enum OperationType
    {
        Unknown = 0,

        Main,

        MessageTools,
        ColumnTools,
        HeaderTools,

        AlterBaseEntity,
        AlterPerson,
        AlterService,

        AlterAddress,
        AlterCategory,
        AlterContactInfo,
        AlterEmail,
        AlterPayCheck,
        AlterPhoneNumber,

        AlterCustomer,
        AlterEmployee ,
        AlterLegalPerson,
        AlterNaturalPerson,
        AlterProduct,
        AlterSale,

        ListBaseEntity,
        ListPerson,
        ListService,

        ListAddress,
        ListCategory,
        ListContactInfo,
        ListEmail,
        ListPayCheck,
        ListPhoneNumber,

        ListCustomer,
        ListEmployee,
        ListLegalPerson,
        ListNaturalPerson,
        ListProduct,
        ListOp,

        SellProduct,
        SellService,
    }

    public static class OperationTypeExtension
    {
        public static OperationType ToOperationType(this string operationType)
        {
            OperationType o;
            if (Enum.TryParse(operationType, out o))
                return o;
            throw new ArgumentException("Not parsable to OperationTypeEnum", "operationType");
        }
    }
}