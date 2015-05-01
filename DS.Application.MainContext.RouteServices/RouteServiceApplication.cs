using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using DS.Application.MainContext.RouteServices.DTO;
using DS.DistributedServices.ServiceProxies.AddressFinder;
using DS.DistributedServices.ServiceProxies.RouteProximity;
using DS.Domain.MainContext;
using DS.Infrastructure.Crosscutting.Util;

namespace DS.Application.MainContext.RouteServices
{
   public class RouteServiceApplication: IDisposable
   {
      private string TOKEN = "c13iyCvmcC9mzwkLd0LCbCBHcXYD5mUA5m2jNGutNXK6NJc6NJt=";

      public RouteServiceResultDTO Process(RouteServiceParameterRequestDTO routeParameterRequest)
      {
         RouteServiceResultDTO routeServiceResult = new RouteServiceResultDTO();
         try
         {
            if (routeParameterRequest == null || routeParameterRequest.originalRoute == null || routeParameterRequest.destinationRoute ==null)
            {
              ThrowFaultException("Invalid Input Parameters !");
            }

            RouteType routeType = routeParameterRequest.routeType;

            //Set GeoLocation Information from Original Route
            SetGeolocationInfo(routeParameterRequest.originalRoute);

            //Get GeoLocation Information from Destination Route
            SetGeolocationInfo(routeParameterRequest.destinationRoute);

            //Mount RouteStop Objects
            RouteStop originalRouteDetails = GetRouteStopInfo(routeParameterRequest.originalRoute);
            RouteStop destinationRouteDetails = GetRouteStopInfo(routeParameterRequest.destinationRoute);

            //Get Route Proximity Options
            var routeProximityOptions = GetRouteProximityOptions(routeType);
            var routes = new[] { originalRouteDetails, destinationRouteDetails};

            //Get Summary Totals
            routeServiceResult = GetRouteProximityTotals(routeProximityOptions, routes);
         }
         catch (Exception)
         {
            ThrowFaultException("Problem while executing Process Call !");
         }

         return routeServiceResult;

      }

      private void ThrowFaultException(string message)
      {
         TransactionExecutionFault transactionExecutionFault = new TransactionExecutionFault(message);
         throw new FaultException<TransactionExecutionFault>(transactionExecutionFault, new FaultReason(transactionExecutionFault.Message));
      }

      private RouteServiceResultDTO GetRouteProximityTotals(RouteProximityOptions routeProximityOptions, RouteStop[] routes)
      {
         RouteServiceResultDTO routeServiceResult = new RouteServiceResultDTO();

         using (var routeProximitySoapClient = new RouteProximitySoapClient())
         {
            var getRouteProximityTotalsResponse = routeProximitySoapClient.getRouteProximityTotals(routes, routeProximityOptions, TOKEN);

            routeServiceResult.TotalDistance = getRouteProximityTotalsResponse.totalDistance;
            routeServiceResult.TotalFuelCost = getRouteProximityTotalsResponse.totalfuelCost;
            routeServiceResult.TotalTime = getRouteProximityTotalsResponse.totalTime;
            routeServiceResult.TotalTollFeeCost = getRouteProximityTotalsResponse.totaltollFeeCost;
         }

         return routeServiceResult;
      }

      private RouteProximityOptions GetRouteProximityOptions(RouteType routeType)
      {
         var routeProximityOptions = new RouteProximityOptions
         {
            language = "portugues",
            routeDetails = new RouteDetails { descriptionType = 1, routeType = (int)routeType, optimizeRoute = true },
            vehicle = new Vehicle
            {
               tankCapacity = 40,
               averageConsumption = 10,
               fuelPrice = 3,
               averageSpeed = 60,
               tollFeeCat = 2
            }
         };

         return routeProximityOptions;
      }

      private RouteStop GetRouteStopInfo(RouteServiceParameterDTO routeDetails)
      {
         var routeStopDetails = new RouteStop()
         {
            description = routeDetails.StreetAvenueName + "," + routeDetails.Number + " - " + routeDetails.City + " - " + routeDetails.State,
            point = new DistributedServices.ServiceProxies.RouteProximity.Point() { x = routeDetails.x, y = routeDetails.y }
         };

         return routeStopDetails;
      }

      private void SetGeolocationInfo(RouteServiceParameterDTO routeInfo)
      {
         Address address = GetAddressInfoObject(routeInfo);
         DS.DistributedServices.ServiceProxies.AddressFinder.Point point = GetGeoLocationFromWebService(address);
         if (point == null)
         {
            ThrowFaultException("Error while processing request !");
         }

         routeInfo.x = point.x;
         routeInfo.y = point.y;
      }

      private DS.DistributedServices.ServiceProxies.AddressFinder.Point GetGeoLocationFromWebService(Address address)
      {
         DS.DistributedServices.ServiceProxies.AddressFinder.Point point = null;
         using (AddressFinderSoapClient addressFinderSoapClient = new AddressFinderSoapClient())
         {
            point = addressFinderSoapClient.getXY(address, TOKEN);
         }

         return point;
      }

      private Address GetAddressInfoObject(RouteServiceParameterDTO routeServiceParameterDTO)
      {
         Address address = null;
         if (routeServiceParameterDTO != null)
         {
            address = new Address()
            {
               street = routeServiceParameterDTO.StreetAvenueName,
               houseNumber = routeServiceParameterDTO.Number,
               city = new DS.DistributedServices.ServiceProxies.AddressFinder.City() { name = routeServiceParameterDTO.City, state = routeServiceParameterDTO.State }
            };
         }

         return address;
      }

      #region IDisposable Members

      public void Dispose()
      {
      }
      #endregion

   }
}
