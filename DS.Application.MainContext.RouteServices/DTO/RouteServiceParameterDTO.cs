using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Application.MainContext.RouteServices.DTO
{
   [Serializable]
   public class RouteServiceParameterDTO
   {
      public string StreetAvenueName { get; set; }
      public string Number { get; set; }
      public string City { get; set; }
      public string State { get; set; }
      public double x { get; set; }
      public double y { get; set; }

      public RouteServiceResultDTO RouteServiceResult { get; set; }

      public string Error { get; set; }
   }
}
