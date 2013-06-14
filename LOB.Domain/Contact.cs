using System;
using LOB.Domain.Base;

namespace LOB.Domain {
    [Serializable]
    public class Contact : Person {
        public string Detail { get; set; }
    }
}