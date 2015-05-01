namespace DS.TestForm
{
   partial class frmTestService
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

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.btnTestService = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // btnTestService
         // 
         this.btnTestService.Location = new System.Drawing.Point(95, 87);
         this.btnTestService.Name = "btnTestService";
         this.btnTestService.Size = new System.Drawing.Size(91, 43);
         this.btnTestService.TabIndex = 0;
         this.btnTestService.Text = "Test It !";
         this.btnTestService.UseVisualStyleBackColor = true;
         this.btnTestService.Click += new System.EventHandler(this.btnTestService_Click);
         // 
         // frmTestService
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(284, 262);
         this.Controls.Add(this.btnTestService);
         this.Name = "frmTestService";
         this.Text = "Test Service !";
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.Button btnTestService;
   }
}

