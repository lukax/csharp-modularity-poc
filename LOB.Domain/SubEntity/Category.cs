#region Usings

using System;
using System.ComponentModel;
using System.Linq;
using LOB.Domain.Base;
using NullGuard;

#endregion

namespace LOB.Domain.SubEntity
{
    [Serializable]
    public class Category : Service
    {
        public override string ToString()
        {
            return Name;
        }

    }
}