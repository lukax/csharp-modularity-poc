#region Usings

using System;
using System.ComponentModel.Composition;
using LOB.UI.Contract.Infrastructure;

#endregion

namespace LOB.UI.Core.View.Infrastructure {
    [MetadataAttribute, AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ViewInfoAttribute : ExportAttribute, IViewInfo {
        public ViewInfoAttribute(ViewType type = ViewType.Other, params ViewState[] states) {
            ViewType = type;
            ViewStates = states;
        }
        public ViewType ViewType { get; set; }
        public ViewState[] ViewStates { get; set; }
    }
}