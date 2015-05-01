using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS.Domain.MainContext;

namespace DS.Application.MainContext.RouteServices.DTO
{
   public class RouteServiceParameterRequestDTO
   {
      public RouteServiceParameterDTO originalRoute { get; set; }
      public RouteServiceParameterDTO destinationRoute { get; set; }
      public RouteType routeType { get; set; }

   }
}
