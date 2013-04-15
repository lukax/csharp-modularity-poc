namespace LOB.UI.Interface.Infrastructure {
    public enum ViewState {
        Internal = 0, //Internal Usage

        //Altering
        Add = 9,
        Update = 27,
        Delete = 81,

        //Listing
        List = 5,
        QuickSearch = 25, //Internal Usage

        //Business
        Sell = 7,
    }
}