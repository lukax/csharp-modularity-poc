#region Usings

using System;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using NullGuard;

#endregion

namespace LOB.UI.Core.View.Controls.Util {
    /// <summary>
    ///     Interaction logic for MessageShowToolView.xaml
    /// </summary>
    public partial class MessageShowToolView : IBaseView {

        public MessageShowToolView() { InitializeComponent(); }

        [AllowNull]
        public IBaseViewModel ViewModel {
            get {
                IBaseViewModel result = null;
                Dispatcher.Invoke(() => result = DataContext as IBaseViewModel);
                return result;
            }
            set {
                IBaseViewModel result = value;
                Dispatcher.Invoke(() => DataContext = result);
                //this.DataContext = value;
                //var vm = value as MessageToolViewModel;
                //if (vm != null)
                //    (vm).PropertyChanged += (sender, args) =>
                //        {
                //            if (vm.IsRestrictive) {
                //                BorderOuter.IsHitTestVisible = true;
                //                GridInner.IsHitTestVisible = true;
                //                BorderInner.IsHitTestVisible = true;
                //                BorderInner.Opacity = .8;
                //                ButtonClose.Visibility = Visibility.Visible;
                //                Progress.Visibility = Visibility.Visible;
                //                BorderOuter.Visibility = Visibility.Visible;
                //            }
                //            else {
                //                BorderOuter.IsHitTestVisible = false;
                //                GridInner.IsHitTestVisible = false;
                //                BorderInner.IsHitTestVisible = false;
                //                BorderInner.Opacity = .6;
                //                ButtonClose.Visibility = Visibility.Hidden;
                //                Progress.Visibility = Visibility.Hidden;
                //                BorderOuter.Visibility = Visibility.Hidden;
                //            }
                //        };
            }
        }

        public string Header {
            get { return Strings.UI_Header_Main_Message; }
        }

        public int Index { get; set; }

        public void InitializeServices() { }

        public void Refresh() { }

        public UIOperation Operation {
            get { return ViewModel.Operation; }
        }
        #region Implementation of IDisposable

        public void Dispose() {
            if(ViewModel != null) ViewModel.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}