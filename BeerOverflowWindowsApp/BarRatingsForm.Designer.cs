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
            beerPanel = new BeerPanel(200, 200, 200);
            this.barRatingsDataGrid = new System.Windows.Forms.DataGridView();
            this.barsComboBox = new System.Windows.Forms.ComboBox();
            this.RatingButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.barRatingsDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // beerPanel
            // 
            this.beerPanel.BackColor = System.Drawing.SystemColors.Window;
            this.beerPanel.Location = new System.Drawing.Point(577, 67);
            this.beerPanel.Name = "beerPanel";
            this.beerPanel.Size = new System.Drawing.Size(403, 476);
            this.beerPanel.TabIndex = 4;
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
            // BarRatingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1223, 595);
            this.Controls.Add(this.beerPanel);
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
        private BeerPanel beerPanel;
    }
}