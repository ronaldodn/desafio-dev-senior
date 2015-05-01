using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Application.MainContext.RouteServices.DTO
{
   [Serializable]
   public class RouteServiceResultDTO
   {
      public string TotalTime { get; set; }
      public double TotalDistance { get; set; }
      public double TotalFuelCost { get; set; }
      public double TotalTollFeeCost { get; set; }
   }
}
