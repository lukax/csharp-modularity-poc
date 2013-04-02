namespace LOB.UI.Interface.Infrastructure {
    public enum UIOperationState {

        Unknown = 0,

        Loading = 1, //Internal Usage

        Alter = 3,
        Add = 9, //Internal Usage
        Update = 27, //Internal Usage
        Discard = 81, //Internal Usage

        List = 5,
        QuickSearch = 25, //Internal Usage

        Sell = 7,

        Tool = 11,

        Exit = 13, //Internal Usage
    }
}