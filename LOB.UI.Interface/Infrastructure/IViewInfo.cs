#region Usings

using System.Linq;
using LOB.Core.Localization;

#endregion

namespace LOB.UI.Contract.Infrastructure {
    public interface IViewInfo {
        ViewType ViewType { get; }
        ViewState[] ViewStates { get; }
    }

    public static class ViewInfoExtension {
        private class InternalViewInfo : IViewInfo {
            public ViewType ViewType { get; set; }
            public ViewState[] ViewStates { get; set; }
        }

        public static bool Equals(this IViewInfo viewInfo, IViewInfo other) {
            if(other == null) return false;
            if(viewInfo == null) return false;
            if(other.ViewStates == null) return false;
            if(viewInfo.ViewStates == null) return false;
            return (viewInfo.ViewType == other.ViewType && viewInfo.ViewStates.SequenceEqual(other.ViewStates));
        }

        public static IViewInfo New(ViewType viewType, ViewState[] viewStates) { return new InternalViewInfo {ViewStates = viewStates, ViewType = viewType}; }
        public static string ToString(this IViewInfo viewInfo) { return string.Format("{0}_{1}", viewInfo.ViewStates.First().ToString(), viewInfo.ViewType.ToString()).ToLocalizedString("Command"); }
    }
}