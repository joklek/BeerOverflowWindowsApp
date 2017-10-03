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
            this.manualBarRating = new BeerOverflowWindowsApp.ManualBarRating();
            this.ButtonSortByTitle = new System.Windows.Forms.Button();
            this.ButtonSortByRating = new System.Windows.Forms.Button();
            this.ButtonSortByDistance = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.barRatingsDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // barRatingsDataGrid
            // 
            this.barRatingsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.barRatingsDataGrid.Location = new System.Drawing.Point(12, 38);
            this.barRatingsDataGrid.Name = "barRatingsDataGrid";
            this.barRatingsDataGrid.Size = new System.Drawing.Size(326, 545);
            this.barRatingsDataGrid.TabIndex = 0;
            this.barRatingsDataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.barRatingsDataGrid_CellClick);
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
            // manualBarRating
            // 
            this.manualBarRating.ImageSize = 100;
            this.manualBarRating.Location = new System.Drawing.Point(555, 397);
            this.manualBarRating.MaximumSize = new System.Drawing.Size(500, 100);
            this.manualBarRating.MinimumSize = new System.Drawing.Size(500, 100);
            this.manualBarRating.Name = "manualBarRating";
            this.manualBarRating.Size = new System.Drawing.Size(500, 100);
            this.manualBarRating.TabIndex = 5;
            this.manualBarRating.Text = "manualBarRating";
            //
            // ButtonSortByTitle
            // 
            this.ButtonSortByTitle.Location = new System.Drawing.Point(12, 12);
            this.ButtonSortByTitle.Name = "ButtonSortByTitle";
            this.ButtonSortByTitle.Size = new System.Drawing.Size(99, 23);
            this.ButtonSortByTitle.TabIndex = 5;
            this.ButtonSortByTitle.Text = "ByTitle";
            this.ButtonSortByTitle.UseVisualStyleBackColor = true;
            this.ButtonSortByTitle.Click += new System.EventHandler(this.ButtonSortByTitle_Click);
            // 
            // ButtonSortByRating
            // 
            this.ButtonSortByRating.Location = new System.Drawing.Point(117, 12);
            this.ButtonSortByRating.Name = "ButtonSortByRating";
            this.ButtonSortByRating.Size = new System.Drawing.Size(108, 23);
            this.ButtonSortByRating.TabIndex = 6;
            this.ButtonSortByRating.Text = "ByRating";
            this.ButtonSortByRating.UseVisualStyleBackColor = true;
            this.ButtonSortByRating.Click += new System.EventHandler(this.ButtonSortByRating_Click);
            // 
            // ButtonSortByDistance
            // 
            this.ButtonSortByDistance.Location = new System.Drawing.Point(231, 12);
            this.ButtonSortByDistance.Name = "ButtonSortByDistance";
            this.ButtonSortByDistance.Size = new System.Drawing.Size(107, 23);
            this.ButtonSortByDistance.TabIndex = 7;
            this.ButtonSortByDistance.Text = "ByDistance";
            this.ButtonSortByDistance.UseVisualStyleBackColor = true;
            this.ButtonSortByDistance.Click += new System.EventHandler(this.ButtonSortByDistance_Click);
            // 
            // BarRatingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1223, 595);
            this.Controls.Add(this.manualBarRating);
            this.Controls.Add(this.ButtonSortByDistance);
            this.Controls.Add(this.ButtonSortByRating);
            this.Controls.Add(this.ButtonSortByTitle);
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
        private ManualBarRating manualBarRating;
        private System.Windows.Forms.Button ButtonSortByTitle;
        private System.Windows.Forms.Button ButtonSortByRating;
        private System.Windows.Forms.Button ButtonSortByDistance;
    }
}