#region Usings

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using LOB.Core.Localization;
using LOB.Domain.Logic;
using LOB.UI.Contract;
using LOB.UI.Contract.Command;
using LOB.UI.Contract.Infrastructure;
using LOB.UI.Contract.ViewModel.Controls.List;
using LOB.UI.Core.Event;
using LOB.UI.Core.Event.View;
using LOB.UI.Core.ViewModel.Base;
using MahApps.Metro.Controls;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List {
    [Export(typeof(IListOpViewModel)), PartCreationPolicy(CreationPolicy.Shared)]
    public sealed class ListOpViewModel : BaseViewModel, IListOpViewModel, IPartImportsSatisfiedNotification {
        private string _search;
        private IDictionary<string, IViewInfo> _viewinfodict;
        private IDictionary<string, IViewInfo> ViewInfoDict {
            get {
                return (_viewinfodict ??
                        (_viewinfodict =
                         LazyViewInfos.Where(x => !x.Metadata.ViewStates.Contains(ViewState.Other))
                                      .ToDictionary(key => ViewInfoExtension.ToString(key.Metadata), val => val.Metadata)));
            }
        }
        public string Entity { get; set; }
        public ObservableCollection<PanoramaGroup> Entities { get; set; }
        public ICommand OpenOpCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public string SearchString {
            get { return (_search ?? "").ToLower(); }
            set {
                _search = value;
                if(String.IsNullOrEmpty(value)) Worker.RunWorkerAsync();
                //if(!Worker.IsBusy) Worker.RunWorkerAsync();
            }
        }
        [ImportMany] public Lazy<IBaseView<IBaseViewModel>, IViewInfo>[] LazyViewInfos { get; set; }
        [Import] public Lazy<IEventAggregator> LazyEventAggregator { get; set; }

        public void OnImportsSatisfied() {
            SearchCommand = new DelegateCommand(SearchExecute);
            OpenOpCommand = new DelegateCommand(OpenOpExecute);
            Entity = "";
            IsChild = false;
            ChangeState(ViewState.Other);
            Unlock();
        }

        private void SearchExecute(object o) { if(!Worker.IsBusy) Worker.RunWorkerAsync(); }

        public override void InitializeServices() {
            Worker.DoWork += UpdateList;
            Worker.RunWorkerAsync();
        }

        private void UpdateList(object sender, DoWorkEventArgs doWorkEventArgs) {
            var worker = sender as BackgroundWorker;
            if(worker == null) return;

            IEnumerable<string> alterGroup;
            IEnumerable<string> listGroup;
            IEnumerable<string> sellGroup;
            if(string.IsNullOrWhiteSpace(SearchString)) {
                alterGroup = (ViewInfoDict.Where(x => x.Value.ViewStates.Contains(ViewState.Add)).Select(x => x.Key));
                listGroup = (ViewInfoDict.Where(x => x.Value.ViewStates.Contains(ViewState.List)).Select(x => x.Key));
                sellGroup = (ViewInfoDict.Where(x => x.Value.ViewStates.Contains(ViewState.Sell)).Select(x => x.Key));
            }
            else {
                alterGroup =
                    (ViewInfoDict.Where(x => x.Value.ViewStates.Contains(ViewState.Add))
                                 .Select(x => x.Key)
                                 .Where(x => x.ToLower().Contains(SearchString)));
                listGroup =
                    (ViewInfoDict.Where(x => x.Value.ViewStates.Contains(ViewState.List))
                                 .Select(x => x.Key)
                                 .Where(x => x.ToLower().Contains(SearchString)));
                sellGroup =
                    (ViewInfoDict.Where(x => x.Value.ViewStates.Contains(ViewState.Sell))
                                 .Select(x => x.Key)
                                 .Where(x => x.ToLower().Contains(SearchString)));
            }
            Entities = new ObservableCollection<PanoramaGroup> { // ReSharper disable PossibleMultipleEnumeration
                alterGroup.Any() ? new PanoramaGroup(Strings.UI_Header_Alter, alterGroup) : null,
                listGroup.Any() ? new PanoramaGroup(Strings.UI_Header_List, listGroup) : null,
                sellGroup.Any() ? new PanoramaGroup(Strings.UI_Header_Sell, sellGroup) : null
                // ReSharper restore PossibleMultipleEnumeration INFO: Not going to affect much the performance
            };
        }

        private void OpenOpExecute(object arg) {
            var viewInfo = ViewInfoDict[arg.ToString()];
            var notification = new Notification {
                Message = string.Format("{0} {1}", Strings.Common_Initializing, ViewInfoDict.FirstOrDefault(x => x.Value.Equals(viewInfo)).Key),
                Progress = -2,
                Type = NotificationType.Info
            };
            LazyEventAggregator.Value.GetEvent<NotificationEvent>().Publish(notification);
            LazyEventAggregator.Value.GetEvent<OpenViewInfoEvent>().Publish(new OpenViewInfoPayload(viewInfo));
            var stringy = string.Format("{0} {1}", Strings.Common_Initialized, ViewInfoDict.FirstOrDefault(x => x.Value.Equals(viewInfo)).Key);
            LazyEventAggregator.Value.GetEvent<NotificationEvent>().Publish(notification.Message(stringy).Progress(-1).State(NotificationType.Ok));
            LazyEventAggregator.Value.GetEvent<CloseViewEvent>().Publish(new CloseViewPayload(Id));
        }

        public override void Refresh() {
            base.Refresh();
            SearchString = "";
        }
    }
}