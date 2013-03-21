#region Usings

using System;

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

        NewBaseEntity,
        NewPerson,
        NewService,

        NewAddress,
        NewCategory,
        NewContactInfo,
        NewEmail,
        NewPayCheck,
        NewPhoneNumber,

        NewCustomer,
        NewEmployee,
        NewLegalPerson,
        NewNaturalPerson,
        NewProduct,
        NewSale,

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
        ListSale,
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