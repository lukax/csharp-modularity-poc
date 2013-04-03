﻿#region Usings

using LOB.Business.Interface.Logic.Base;
using LOB.Domain;

#endregion

namespace LOB.Business.Interface.Logic {
    public interface IStoreFacade : IBaseEntityFacade {

        Store GenerateEntity();

    }
}