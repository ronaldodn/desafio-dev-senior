using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using DS.WindowsServices.RouteServices;

namespace DS.WindowsServices.Core
{
   public class Program
   {
      #region Membros privados
      #endregion

      #region Propriedades



      #endregion

      #region Construtores



      #endregion

      #region Métodos privados

      private static void RunAsConsole()
      {
         Console.WriteLine("Mode Console, press key <enter> to exit!");
         Console.WriteLine();
         Console.WriteLine("Starting the Route Service Provider");

         ServiceMain mainService = new ServiceMain(false);
         mainService.StartServiceHost();

         Console.ReadLine();

         mainService.StopServiceHost();

         Console.WriteLine();
         Console.WriteLine("The server has been stopped!");
      }

      private static void RunAsService()
      {
         ServiceBase[] servicesToRun;
         servicesToRun = new ServiceBase[] { new ServiceMain(true) };

         ServiceBase.Run(servicesToRun);
      }

      #endregion

      #region Métodos públicos

      private static void Main(string[] args)
      {

         if (args != null && args.Length > 0)
         {
            if (args[0].Equals("-i", StringComparison.OrdinalIgnoreCase))
            {
               SelfInstaller.InstallMe();
               return;
            }
            else if (args[0].Equals("-u", StringComparison.OrdinalIgnoreCase))
            {
               SelfInstaller.UninstallMe();
               return;
            }
            else if (args[0].Equals("-c", StringComparison.OrdinalIgnoreCase))
            {
               RunAsConsole();
            }
            else
            {
               Console.WriteLine(args[0]);
            }
         }
         else
         {
#if DEBUG
            if (Debugger.IsAttached == true)
               RunAsConsole();
#else
            RunAsService();
#endif
         }
      }


      #endregion

   }
}
