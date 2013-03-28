﻿#region Usings

using LOB.Business.Interface.Logic.Base;
using LOB.Domain;

#endregion

namespace LOB.Business.Interface.Logic.SubEntity
{
    public interface IPayCheckFacade : IBaseEntityFacade
    {
        new void SetEntity<T>(T entity) where T : PayCheck;
    }
}