#region Usings

using System;

#endregion

namespace LOB.UI.Interface.Infrastructure {
    public struct UIOperation {

        public UIOperationType Type { get; set; }
        public UIOperationState State { get; set; }

        public override string ToString() {
            return string.Format("{0}_{1}", State.ToString(), Type.ToString());
        }
        #region Equality members

        public bool Equals(UIOperation other) {
            return this == other;
        }

        public override int GetHashCode() {
            unchecked {
                return ((int) Type*397) ^ (int) State;
            }
        }

        public override bool Equals(object obj) {
            return obj is UIOperation && (UIOperation) obj == this;
        }

        public static bool operator ==(UIOperation s, UIOperation s2) {
            return (s.Type == s2.Type && s.State == s2.State);
        }

        public static bool operator !=(UIOperation s, UIOperation s2) {
            return !(s == s2);
        }

        #endregion
    }

    public static class UIOperationExtensions {

        public static UIOperation ToUIOperation(this string s) {
            string[] cutted = s.Split('_');
            UIOperationState parsedState;
            UIOperationType parsedType;
            if(Enum.TryParse(cutted[0], out parsedState)) 
                if(Enum.TryParse(cutted[1], out parsedType)) 
                    return new UIOperation {State = parsedState, Type = parsedType};
            throw new ArgumentException("s");
        }

    }
}