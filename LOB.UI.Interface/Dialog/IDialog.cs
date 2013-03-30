namespace LOB.UI.Interface.Dialog {
    public interface IDialog {

        bool? ShowDialogMessage(string title, string message);
        bool? ShowDialogView(object view, object viewModel);
        void ShowMessage(string title, string message);
        void ShowView(object view, object viewModel);

    }
}