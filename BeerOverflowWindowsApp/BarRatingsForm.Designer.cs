namespace BeerOverflowWindowsApp
{
    partial class BarRatingsForm
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
            this.barRatingsDataGrid = new System.Windows.Forms.DataGridView();
            this.barsComboBox = new System.Windows.Forms.ComboBox();
            this.RatingButton = new System.Windows.Forms.Button();
            this.manualBeerMeter = new BeerOverflowWindowsApp.ManualBeerMeter();
            this.manualBarRating1 = new BeerOverflowWindowsApp.ManualBarRating();
            ((System.ComponentModel.ISupportInitialize)(this.barRatingsDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // barRatingsDataGrid
            // 
            this.barRatingsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.barRatingsDataGrid.Location = new System.Drawing.Point(12, 12);
            this.barRatingsDataGrid.Name = "barRatingsDataGrid";
            this.barRatingsDataGrid.Size = new System.Drawing.Size(326, 571);
            this.barRatingsDataGrid.TabIndex = 0;
            // 
            // barsComboBox
            // 
            this.barsComboBox.FormattingEnabled = true;
            this.barsComboBox.Location = new System.Drawing.Point(344, 12);
            this.barsComboBox.Name = "barsComboBox";
            this.barsComboBox.Size = new System.Drawing.Size(121, 21);
            this.barsComboBox.TabIndex = 2;
            // 
            // RatingButton
            // 
            this.RatingButton.Location = new System.Drawing.Point(489, 12);
            this.RatingButton.Name = "RatingButton";
            this.RatingButton.Size = new System.Drawing.Size(75, 23);
            this.RatingButton.TabIndex = 3;
            this.RatingButton.Text = "Rate";
            this.RatingButton.UseVisualStyleBackColor = true;
            this.RatingButton.Click += new System.EventHandler(this.RatingButton_Click);
            // 
            // manualBeerMeter
            // 
            this.manualBeerMeter.Location = new System.Drawing.Point(728, 24);
            this.manualBeerMeter.markerHeight = 50;
            this.manualBeerMeter.markerWidth = 150;
            this.manualBeerMeter.Name = "manualBeerMeter";
            this.manualBeerMeter.Size = new System.Drawing.Size(318, 332);
            this.manualBeerMeter.TabIndex = 4;
            this.manualBeerMeter.Text = "manualBeerMeter";
            // 
            // manualBarRating1
            // 
            this.manualBarRating1.ImageSize = 100;
            this.manualBarRating1.Location = new System.Drawing.Point(506, 426);
            this.manualBarRating1.MinimumSize = new System.Drawing.Size(600, 100);
            this.manualBarRating1.Name = "manualBarRating1";
            this.manualBarRating1.Size = new System.Drawing.Size(600, 100);
            this.manualBarRating1.TabIndex = 5;
            this.manualBarRating1.Text = "manualBarRating1";
            // 
            // BarRatingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1223, 595);
            this.Controls.Add(this.manualBarRating1);
            this.Controls.Add(this.manualBeerMeter);
            this.Controls.Add(this.RatingButton);
            this.Controls.Add(this.barsComboBox);
            this.Controls.Add(this.barRatingsDataGrid);
            this.Name = "BarRatingsForm";
            this.Text = "BarRatings";
            ((System.ComponentModel.ISupportInitialize)(this.barRatingsDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView barRatingsDataGrid;
        private System.Windows.Forms.ComboBox barsComboBox;
        private System.Windows.Forms.Button RatingButton;
        private ManualBeerMeter manualBeerMeter;
        private ManualBarRating manualBarRating1;
    }
}