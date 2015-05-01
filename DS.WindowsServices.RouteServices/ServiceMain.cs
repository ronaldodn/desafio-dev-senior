using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS.DistributedServices.RouteServices.ServiceHost;
using DS.WindowsServices.Seedwork;

namespace DS.WindowsServices.RouteServices
{
   partial class ServiceMain : MainService<RouteService>
   {
      #region Construtores

      public ServiceMain(bool startAsService)
         : base(startAsService)
      {
      }

      #endregion

   }

   partial class ServiceMain
   {
      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Component Designer generated code

      /// <summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </summary>
      protected override void InitializeComponent()
      {
         components = new System.ComponentModel.Container();
         this.ServiceName = "Route Services";
      }

      #endregion
   }

}
