using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using DS.Application.MainContext.RouteServices;
using DS.DistributedServices.RouteServices.Interfaces;

namespace DS.DistributedServices.RouteServices.ServiceHost
{
   [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                   ConcurrencyMode = ConcurrencyMode.Multiple)]
   public class RouteService: IRouteService
   {

      public Application.MainContext.RouteServices.DTO.RouteServiceResultDTO GetRoute(Application.MainContext.RouteServices.DTO.RouteServiceParameterRequestDTO routeParameterRequest)
      {
         try
         {
            Application.MainContext.RouteServices.DTO.RouteServiceResultDTO routeServiceResult;
            using (RouteServiceApplication routeServiceApplication = new RouteServiceApplication())
            {
               routeServiceResult = routeServiceApplication.Process(routeParameterRequest);
            };
            return routeServiceResult;
         }
         catch (Exception ex)
         {
            throw;
         }
      }
   }
}
