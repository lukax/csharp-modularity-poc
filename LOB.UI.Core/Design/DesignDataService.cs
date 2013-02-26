#region Usings

using System;
using LOB.UI.Core.Model;

#endregion

namespace LOB.UI.Core.Design
{
    public class DesignDataService : IDataService
    {
        public void GetData(Action<DataItem, Exception> callback) {
            // Use this to create design time data

            var item = new DataItem("Welcome to MVVM Light [design]");
            callback(item, null);
        }
    }
}