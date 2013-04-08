using LOB.Domain.Base;

namespace LOB.UI.Interface.Infrastructure {
    public enum UIOperationState {

        Internal = 0, //Internal Usage

        //Altering
        Add = 9,
        Update = 27,
        Discard = 81,

        //Listing
        List = 5,
        QuickSearch = 25, //Internal Usage

        //Business
        Sell = 7,

    }
}