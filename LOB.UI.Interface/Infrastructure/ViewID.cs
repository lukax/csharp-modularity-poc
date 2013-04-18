#region Usings

using System;
using LOB.Domain.Base;

//using NullGuard;

#endregion

namespace LOB.UI.Interface.Infrastructure {
    public class ViewID : BaseNotifyChange, IEquatable<ViewID> {
        public ViewID() {
            IsChild = true;
            ViewModel = null;
        }
        public ViewType Type { get; set; }
        public ViewState State { get; set; }
        public ViewSubState SubState { get; set; }
        public bool IsChild { get; set; }

        public IBaseViewModel ViewModel { get; set; }
        public override string ToString() { return string.Format("{0}_{1}", State.ToString(), Type.ToString()); }
        #region Equality members

        public bool Equals(ViewID other) {
            if(ReferenceEquals(other, null)) return false;
            return (Type == other.Type && State == other.State);
        }

        public override int GetHashCode() {
            unchecked {
                return ((int)Type*397) ^ (int)State;
            }
        }

        public override bool Equals(object obj) {
            var local = obj as ViewID;
            return local != null && Equals(local);
        }

        #endregion
    }

    public enum ViewType {
        Unknown = 0,

        Main,

        MessageTool,
        NotificationTool,
        ColumnTool,
        HeaderTool,

        BaseEntity,
        Person,
        Service,

        Address,
        Category,
        ContactInfo,
        Email,
        PayCheck,
        PhoneNumber,

        Customer,
        Employee,
        LegalPerson,
        NaturalPerson,
        Product,
        Sale,
        Store,

        Op,
    }

    public enum ViewState {
        Internal = 0, //Internal Usage

        //Altering
        Add = 9,
        Update = 27,
        Delete = 81,

        //Listing
        List = 5,
        QuickSearch = 25, //Internal Usage

        //Business
        Sell = 7,
    }

    public enum ViewSubState {
        Unlocked,
        Locked,
    }

    public static class ViewIDExtension {
        public static ViewID ToUIOperation(this string s) {
            string[] cutted = s.Split('_');
            ViewState parsedState;
            ViewType parsedType;
            if(Enum.TryParse(cutted[0], out parsedState)) if(Enum.TryParse(cutted[1], out parsedType)) if(Enum.TryParse(cutted[3], out parsedType)) return new ViewID {State = parsedState, Type = parsedType};
            throw new ArgumentException("s");
        }
        #region Fluent methods for ViewID

        public static ViewID Type(this ViewID op, ViewType type) {
            op.Type = type;
            return op;
        }
        public static ViewID State(this ViewID op, ViewState state) {
            op.State = state;
            return op;
        }
        public static ViewID SubState(this ViewID op, ViewSubState subState) {
            op.SubState = subState;
            return op;
        }
        public static ViewID IsChild(this ViewID op, bool isChild) {
            op.IsChild = isChild;
            return op;
        }
        public static ViewID ViewModel(this ViewID op, IBaseViewModel viewModel) {
            op.ViewModel = viewModel;
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