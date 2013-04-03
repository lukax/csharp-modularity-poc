namespace LOB.UI.Interface.Infrastructure {
    public interface IUIComponent {

        UIOperation Operation { get; }
        string Header { get; }

    }
}