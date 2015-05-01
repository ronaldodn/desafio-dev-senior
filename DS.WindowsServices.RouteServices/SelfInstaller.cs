using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DS.WindowsServices.RouteServices
{
   public static class SelfInstaller
   {

      #region Membros privados

      private static readonly string _ExePath = Assembly.GetExecutingAssembly().Location;

      #endregion

      #region Propriedades



      #endregion

      #region Construtores

      #endregion

      #region Métodos privados



      #endregion

      #region Métodos públicos

      public static bool InstallMe()
      {
         try
         {
            ManagedInstallerClass.InstallHelper(new string[] { _ExePath });
         }
         catch (Exception ex)
         {
            //LoggerX.Error(ex);
            return false;
         }
         return true;
      }

      public static bool UninstallMe()
      {
         try
         {
            ManagedInstallerClass.InstallHelper(new string[] { "/u", _ExePath });
         }
         catch (Exception ex)
         {
            //LoggerX.Error(ex);
            return false;
         }
         return true;
      }
      #endregion
   }
}
