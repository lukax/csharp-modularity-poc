#region Usings

using System;

#endregion

namespace LOB.UI.Core.Model
{
    public interface IDataService
    {
        void GetData(Action<DataItem, Exception> callback);
    }
}