using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using LOB.Domain.SubEntity;

namespace LOB.Service.Contract
{
    [ServiceContract]
    public interface ICategoryService {

        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        DCO.Category One(long code);

        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<DCO.Category> All();
        
    }
}
