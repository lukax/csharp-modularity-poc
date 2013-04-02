namespace LOB.Core.Util {
    public class StringWrapper {

        public StringWrapper(string s) { Value = s; }

        public string Value { get; set; }

        protected bool Equals(StringWrapper other) { return string.Equals(Value, other.Value); }

        public override string ToString() { return Value; }

        public static implicit operator string(StringWrapper e) { return e.Value; }

        public static implicit operator StringWrapper(string value) { return new StringWrapper(value); }

        public override bool Equals(object obj) { return string.Equals(Value, obj.ToString()); }

        public override int GetHashCode() { return (Value != null ? Value.GetHashCode() : 0); }

        public static bool operator ==(StringWrapper s, string s2) { return s != null && s.Value == s2; }

        public static bool operator !=(StringWrapper s, string s2) { return !(s == s2); }

    }
}