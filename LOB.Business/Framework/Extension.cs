#region Usings

using LOB.Domain.Base;

#endregion

namespace LOB.Business.Framework {
    public static class Extension {

        public static bool IsEven(this int i) { return i%2 == 0; }

        public static bool IsOdd(this int i) { return !IsEven(i); }

        public static T Empty<T>(this BaseEntity entity) where T : BaseEntity, new() { return new T(); }

    }
}