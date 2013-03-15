#region Usings

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace LOB.UI.Interface.Infrastructure
{
    public static class Operation
    {
        public const string MessageHideEvent = "MessageHideEvent";
        public const string MessageShowEvent = "MessageShowEvent";

        public const string ColumnTools = "ColumnTools";
        public const string HeaderTools = "HeaderTools";

        public const string AlterBaseEntity = "AlterBaseEntity";
        public const string AlterPerson = "AlterPerson";
        public const string AlterService = "AlterService";

        public const string AlterAddress = "AlterAddress";
        public const string AlterCategory = "AlterCategory";
        public const string AlterContactInfo = "AlterContactInfo";
        public const string AlterEmail = "AlterEmail";
        public const string AlterPayCheck = "AlterPayCheck";
        public const string AlterPhoneNumber = "AlterPhoneNumber";

        public const string AlterCustomer = "AlterCustomer";
        public const string AlterEmployee = "AlterEmployee";
        public const string AlterLegalPerson = "AlterLegalPerson";
        public const string AlterNaturalPerson = "AlterNaturalPerson";
        public const string AlterProduct = "AlterProduct";
        public const string AlterSale = "AlterSale";

        public const string ListBaseEntity = "ListBaseEntity";
        public const string ListPerson = "ListPerson";
        public const string ListService = "ListService";

        public const string ListAddress = "ListAddress";
        public const string ListCategory = "ListCategory";
        public const string ListContactInfo = "ListContactInfo";
        public const string ListEmail = "ListEmail";
        public const string ListPayCheck = "ListPayCheck";
        public const string ListPhoneNumber = "ListPhoneNumber";

        public const string ListCustomer = "ListCustomer";
        public const string ListEmployee = "ListEmployee";
        public const string ListLegalPerson = "ListLegalPerson";
        public const string ListNaturalPerson = "ListNaturalPerson";
        public const string ListProduct = "ListProduct";
        public const string ListOp = "ListOp";

        private static readonly Lazy<IList<string>> LazyList =
            new Lazy<IList<string>>(
                () => typeof (Operation).GetFields().Select(var => var.GetValue(var).ToString()).ToList());

        public static IList<string> All
        {
            get { return LazyList.Value; }
        }
    }
}