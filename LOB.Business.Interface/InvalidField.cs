using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOB.Business.Interface
{
    public class InvalidField
    {
        public InvalidField(string fieldName, string errorDescription)
        {
            FieldName = fieldName;
            ErrorDescription = errorDescription;
        }
        public string FieldName { get; private set; }
        public string ErrorDescription { get; private set; }
    }
}
