using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using DS.DistributedServices.RouteServices.Interfaces;
using DS.DistributedServices.Seekwork;

namespace DS.WindowsServices.Seedwork
{
   public abstract class MainService<T> : ServiceBase where T : IRouteService
   {
      #region Membros Privados

      private ServiceHost serviceHost;

      #endregion

      #region Propriedades
      protected string PrefixBinding
      {
         get 
         {
            return ConfigurationManager.AppSettings["PrefixBinding"];
         }
      }

      protected string GetBaseAddress()
      {
         return ConfigurationManager.AppSettings["ServiceBaseAddress"];
      }

      #endregion

      #region Metodos Abstratos

      protected abstract void InitializeComponent();

      #endregion

      #region Construtores

      public MainService(bool startAsService)
      {
         if (startAsService)
         {
            // service
            InitializeComponent();
            InitServiceHost();
         }
         else
         {
            // console
            InitServiceHost();
         }
      }

      #endregion

      #region Metodos Privados

      private string GetBindingName()
      {
         return PrefixBinding + "TransactionBinding";
      }

      protected override void OnStart(string[] args)
      {
         StartServiceHost();
      }

      protected override void OnStop()
      {
         StopServiceHost();
         base.OnStop();
      }

      protected override void OnShutdown()
      {
         StopServiceHost();
         base.OnShutdown();
      }

      #endregion

      #region Metodos Publicos

      public void InitServiceHost()
      {
         if (serviceHost == null)
         {
            serviceHost = ServiceProxy.GetServiceHost<T, IRouteService>(
               GetBindingName(),
               GetBaseAddress());
         }
      }

      public void StartServiceHost()
      {
         if (serviceHost != null)
            serviceHost.Open();
      }

      public void StopServiceHost()
      {
         if (serviceHost != null)
         {
            serviceHost.Close();
         }
      }


      #endregion

   }
}
