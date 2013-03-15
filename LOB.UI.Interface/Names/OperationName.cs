using System;

namespace LOB.UI.Interface.Names
{
    public enum OperationName
    {
        MessageHideEvent,
        MessageShowEvent,

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
        AlterEmployee,
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
    }

    public static class OperationNamesParser
    {
        public static OperationName Parse(string operationNames)
        {
            try
            {
                OperationName parsed;
                Enum.TryParse<OperationName>(operationNames, out parsed);
                return parsed;
            }
            catch
            {
                throw;
            }
        }
    }
}
