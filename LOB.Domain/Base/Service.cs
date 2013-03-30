#region Usings

using System;

#endregion

namespace LOB.Domain.Base {
    [Serializable] public abstract class Service : BaseEntity {

        public string Name { get; set; }
        public string Description { get; set; }

    }
}