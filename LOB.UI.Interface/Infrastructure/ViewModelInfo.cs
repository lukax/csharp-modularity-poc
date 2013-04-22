#region Usings

using System;
using LOB.Domain.Base;

//using NullGuard;

#endregion

namespace LOB.UI.Interface.Infrastructure {
    public class ViewModelInfo : BaseNotifyChange, IEquatable<ViewModelInfo> {
        public ViewState ViewState { get; set; }
        public ViewSubState ViewSubState { get; set; }
        public bool IsChild { get; set; }
        public override string ToString() { return string.Format("{0}_{1}", ViewState.ToString(), ViewSubState.ToString()); }
        #region Equality members

        public bool Equals(ViewModelInfo other) {
            if(ReferenceEquals(other, null)) return false;
            return (IsChild == other.IsChild && ViewState == other.ViewState && ViewSubState == other.ViewSubState);
        }

        public override int GetHashCode() {
            unchecked {
                return ((int)ViewState*(int)ViewSubState*397) ^ (int)ViewState + (IsChild ? 1 : 2);
            }
        }

        public override bool Equals(object obj) {
            var local = obj as ViewModelInfo;
            return local != null && Equals(local);
        }

        #endregion
    }

    public static class ViewIDExtension {
        public static ViewModelInfo ToUIOperation(this string s) {
            string[] cutted = s.Split('_');
            ViewState parsedState;
            ViewSubState parsedType;
            if(Enum.TryParse(cutted[0], out parsedState)) if(Enum.TryParse(cutted[1], out parsedType)) if(Enum.TryParse(cutted[3], out parsedType)) return new ViewModelInfo {ViewState = parsedState, ViewSubState = parsedType};
            throw new ArgumentException("s");
        }
        #region Fluent methods for ViewModelInfo

        public static ViewModelInfo State(this ViewModelInfo op, ViewState state) {
            op.ViewState = state;
            return op;
        }
        public static ViewModelInfo SubState(this ViewModelInfo op, ViewSubState subState) {
            op.ViewSubState = subState;
            return op;
        }
        public static ViewModelInfo IsChild(this ViewModelInfo op, bool isChild) {
            op.IsChild = isChild;
            return op;
        }

        #endregion
    }

    public static class ViewTypeExtension {
        public static ViewType ToUIOperationType(this string operationType) {
            ViewType o;
            if(Enum.TryParse(operationType, out o)) return o;
            throw new ArgumentException("Not parsable to OperationTypeEnum", "operationType");
        }
    }
}