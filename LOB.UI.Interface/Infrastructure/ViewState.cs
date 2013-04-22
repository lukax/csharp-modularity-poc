namespace LOB.UI.Interface.Infrastructure {
    public enum ViewState {
        Other = 0, //Other Usage

        //Altering
        Add = 9,
        Update = 27,
        Delete = 81,

        //Listing
        List = 5,
        QuickSearch = 25, //Other Usage

        //Business
        Sell = 7,
    }

    public enum ViewSubState {
        Unlocked,
        Locked,
    }

    public enum ViewType
    {
        Other = 0,

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
        Supplier,

        Op,
    }
}