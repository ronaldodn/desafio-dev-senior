using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DS.TestForm.RouteService;

namespace DS.TestForm
{
   public partial class frmTestService : Form
   {
      public frmTestService()
      {
         InitializeComponent();
      }

      private void btnTestService_Click(object sender, EventArgs e)
      {
         RouteService.IRouteService RouteService = new RouteServiceClient();
         RouteServiceParameterDTO originalroute = new RouteServiceParameterDTO();
         //originalroute.StreetAvenueName = "Rua XYZ de ABC";
         originalroute.StreetAvenueName = "Rua Jose Fabiano Rodrigues";
         originalroute.Number = "10";
         originalroute.City = "Osasco";
         originalroute.State = "SP";

         RouteServiceParameterDTO destinationroute = new RouteServiceParameterDTO();
         destinationroute.StreetAvenueName = "Avenida Manoel Pedro Pimentel";
         destinationroute.Number = "155";
         destinationroute.City = "Osasco";
         destinationroute.State = "SP";

         GetRouteRequest inValue = new GetRouteRequest();
         inValue.Body = new GetRouteRequestBody();
         RouteServiceParameterRequestDTO routeServiceParameterRequestDTO = new RouteServiceParameterRequestDTO();
         routeServiceParameterRequestDTO.originalRoute = originalroute;
         routeServiceParameterRequestDTO.destinationRoute = destinationroute;
         routeServiceParameterRequestDTO.routeType = RouteType.FastestRoute;
         inValue.Body.routeParameterRequest = routeServiceParameterRequestDTO;

         try
         {
            GetRouteResponse retVal = RouteService.GetRoute(inValue);
            string result = "Distancia Total {0}. Custo Combustivel {1}. Tempo Total Rota {2}. Custo Considerando Pedagio {3}";
            if (retVal.Body.GetRouteResult != null)
            {
               MessageBox.Show(string.Format(result, retVal.Body.GetRouteResult.TotalDistance.ToString(),
                             retVal.Body.GetRouteResult.TotalFuelCost.ToString(),
                             retVal.Body.GetRouteResult.TotalTime.ToString(),
                             retVal.Body.GetRouteResult.TotalTollFeeCost.ToString()));
            }
            
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.ToString());
         }

      }
   }
}
