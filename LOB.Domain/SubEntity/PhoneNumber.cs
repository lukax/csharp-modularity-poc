﻿#region Usings

using System;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain.SubEntity
{
    [Serializable]
    public class PhoneNumber : BaseEntity
    {
        public virtual int Number { get; set; }
        public virtual NumberType NumberType { get; set; }
        public virtual string Description { get; set; }
    }

    [Serializable]
    public enum NumberType
    {
        Telephone,
        Cellphone,
        Fax
    }
}