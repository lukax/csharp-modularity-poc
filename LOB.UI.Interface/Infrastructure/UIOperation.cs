#region Usings

using System;
using LOB.Domain.Base;
using PropertyChanged;

#endregion

namespace LOB.UI.Interface.Infrastructure {
    public class UIOperation : BaseNotifyChange, IEquatable<UIOperation> {

        public UIOperation() { IsChild = true; }
        public UIOperationType Type { get; set; }
        public UIOperationState State { get; set; }
        public UIOperationStatus Status { get; set; }
        public bool IsChild { get; set; }

        public BaseEntity Entity { get; set; } //TODO: Find a better way for this
        public override string ToString() { return string.Format("{0}_{1}", State.ToString(), Type.ToString()); }
        #region Equality members

        public bool Equals(UIOperation other) { return (Type == other.Type && State == other.State); }

        public override int GetHashCode() {
            unchecked {
                return ((int)Type*397) ^ (int)State;
            }
        }

        public override bool Equals(object obj) {
            var local = obj as UIOperation;
            return local != null && Equals(local);
        }

        //Before convertion to class
        //public static bool operator ==(UIOperation s, UIOperation s2) {
        //    return (s.Type == s2.Type && s.State == s2.State);
        //}

        //public static bool operator !=(UIOperation s, UIOperation s2) { return !(s == s2); }

        #endregion
    }

    public static class UIOperationExtensions {

        public static UIOperation ToUIOperation(this string s) {
            string[] cutted = s.Split('_');
            UIOperationState parsedState;
            UIOperationType parsedType;
            if(Enum.TryParse(cutted[0], out parsedState)) if(Enum.TryParse(cutted[1], out parsedType)) if(Enum.TryParse(cutted[3], out parsedType)) return new UIOperation {State = parsedState, Type = parsedType};
            throw new ArgumentException("s");
        }
        #region Fluent methods for UIOperation

        public static UIOperation Type(this UIOperation op, UIOperationType type) {
            op.Type = type;
            return op;
        }
        public static UIOperation State(this UIOperation op, UIOperationState state) {
            op.State = state;
            return op;
        }
        public static UIOperation Status(this UIOperation op, UIOperationStatus status) {
            op.Status = status;
            return op;
        }
        public static UIOperation IsChild(this UIOperation op, bool isChild) {
            op.IsChild = isChild;
            return op;
        }

        #endregion
    }
}