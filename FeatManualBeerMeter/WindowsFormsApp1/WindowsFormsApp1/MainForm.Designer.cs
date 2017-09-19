namespace WindowsFormsApp1
{
    partial class MainForm
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
            this.beerPanel = new BeerPanel(350, 100, 300);
            this.SuspendLayout();
            // 
            // beerPanel
            // 
            this.beerPanel.BackColor = System.Drawing.SystemColors.Window;
            this.beerPanel.Location = new System.Drawing.Point(0, 0);
            this.beerPanel.Name = "beerPanel";
            this.beerPanel.Size = new System.Drawing.Size(700, 700);
            this.beerPanel.TabIndex = 0;
            //this.beerPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.beerPanel_Paint);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 552);
            this.Controls.Add(this.beerPanel);
            this.Name = "MainForm";
            this.Text = "mainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private BeerPanel beerPanel;
    }
}

