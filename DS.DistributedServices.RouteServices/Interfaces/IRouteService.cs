using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ServiceModel;
using DS.Application.MainContext.RouteServices.DTO;

namespace DS.DistributedServices.RouteServices.Interfaces
{
   [ServiceContract]
   public interface IRouteService
   {
      [OperationContract]
      [XmlSerializerFormat]
      [FaultContract(typeof(TransactionExecutionFault))]
      RouteServiceResultDTO GetRoute(RouteServiceParameterRequestDTO routeParameterRequest);
   }
}
