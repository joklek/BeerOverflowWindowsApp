namespace BeerOverflowWindowsApp
{
    partial class GetBarList
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
            this.resultTextBox = new System.Windows.Forms.TextBox();
            this.radiusLabel = new System.Windows.Forms.Label();
            this.latitudeLabel = new System.Windows.Forms.Label();
            this.radiusTextBox = new System.Windows.Forms.TextBox();
            this.latitudeBox = new System.Windows.Forms.TextBox();
            this.longitudeLabel = new System.Windows.Forms.Label();
            this.longitudeBox = new System.Windows.Forms.TextBox();
            this.Go = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // resultTextBox
            // 
            this.resultTextBox.Location = new System.Drawing.Point(423, 24);
            this.resultTextBox.Multiline = true;
            this.resultTextBox.Name = "resultTextBox";
            this.resultTextBox.Size = new System.Drawing.Size(288, 515);
            this.resultTextBox.TabIndex = 15;
            // 
            // radiusLabel
            // 
            this.radiusLabel.AutoSize = true;
            this.radiusLabel.Location = new System.Drawing.Point(31, 77);
            this.radiusLabel.Name = "radiusLabel";
            this.radiusLabel.Size = new System.Drawing.Size(40, 13);
            this.radiusLabel.TabIndex = 14;
            this.radiusLabel.Text = "Radius";
            // 
            // latitudeLabel
            // 
            this.latitudeLabel.AutoSize = true;
            this.latitudeLabel.Location = new System.Drawing.Point(31, 51);
            this.latitudeLabel.Name = "latitudeLabel";
            this.latitudeLabel.Size = new System.Drawing.Size(45, 13);
            this.latitudeLabel.TabIndex = 13;
            this.latitudeLabel.Text = "Latitude";
            // 
            // radiusTextBox
            // 
            this.radiusTextBox.Location = new System.Drawing.Point(105, 77);
            this.radiusTextBox.Name = "radiusTextBox";
            this.radiusTextBox.Size = new System.Drawing.Size(100, 20);
            this.radiusTextBox.TabIndex = 12;
            // 
            // latitudeBox
            // 
            this.latitudeBox.Location = new System.Drawing.Point(105, 51);
            this.latitudeBox.Name = "latitudeBox";
            this.latitudeBox.Size = new System.Drawing.Size(100, 20);
            this.latitudeBox.TabIndex = 11;
            // 
            // longitudeLabel
            // 
            this.longitudeLabel.AutoSize = true;
            this.longitudeLabel.Location = new System.Drawing.Point(31, 27);
            this.longitudeLabel.Name = "longitudeLabel";
            this.longitudeLabel.Size = new System.Drawing.Size(54, 13);
            this.longitudeLabel.TabIndex = 10;
            this.longitudeLabel.Text = "Longitude";
            // 
            // longitudeBox
            // 
            this.longitudeBox.Location = new System.Drawing.Point(105, 24);
            this.longitudeBox.Name = "longitudeBox";
            this.longitudeBox.Size = new System.Drawing.Size(100, 20);
            this.longitudeBox.TabIndex = 9;
            // 
            // Go
            // 
            this.Go.Location = new System.Drawing.Point(190, 128);
            this.Go.Name = "Go";
            this.Go.Size = new System.Drawing.Size(75, 23);
            this.Go.TabIndex = 8;
            this.Go.Text = "Go";
            this.Go.UseVisualStyleBackColor = true;
            this.Go.Click += new System.EventHandler(this.Go_ClickAsync);
            // 
            // GetBarList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 581);
            this.Controls.Add(this.resultTextBox);
            this.Controls.Add(this.radiusLabel);
            this.Controls.Add(this.latitudeLabel);
            this.Controls.Add(this.radiusTextBox);
            this.Controls.Add(this.latitudeBox);
            this.Controls.Add(this.longitudeLabel);
            this.Controls.Add(this.longitudeBox);
            this.Controls.Add(this.Go);
            this.Name = "GetBarList";
            this.Text = "GetBarData";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox resultTextBox;
        private System.Windows.Forms.Label radiusLabel;
        private System.Windows.Forms.Label latitudeLabel;
        private System.Windows.Forms.TextBox radiusTextBox;
        private System.Windows.Forms.TextBox latitudeBox;
        private System.Windows.Forms.Label longitudeLabel;
        private System.Windows.Forms.TextBox longitudeBox;
        private System.Windows.Forms.Button Go;
    }
}

