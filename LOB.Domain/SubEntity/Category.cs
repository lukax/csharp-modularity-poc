#region Usings
using System;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain.SubEntity {
    [Serializable] public class Category : Service {

        public override string ToString() {
            return this.Name;
        }

    }
}