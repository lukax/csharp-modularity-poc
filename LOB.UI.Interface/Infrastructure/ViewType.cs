#region Usings

using System;

#endregion

namespace LOB.UI.Interface.Infrastructure {
    public enum ViewType {
        Unknown = 0,

        Main,

        MessageTool,
        NotificationTool,
        ColumnTool,
        HeaderTool,

        BaseEntity,
        Person,
        Service,

        Address,
        Category,
        ContactInfo,
        Email,
        PayCheck,
        PhoneNumber,

        Customer,
        Employee,
        LegalPerson,
        NaturalPerson,
        Product,
        Sale,
        Store,

        Op,
    }

    public static class OperationTypeExtension {
        public static ViewType ToUIOperationType(this string operationType) {
            ViewType o;
            if(Enum.TryParse(operationType, out o)) return o;
            throw new ArgumentException("Not parsable to OperationTypeEnum", "operationType");
        }
    }
}