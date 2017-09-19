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
            this.ratingTextBox = new System.Windows.Forms.TextBox();
            this.barsComboBox = new System.Windows.Forms.ComboBox();
            this.ratingButton = new System.Windows.Forms.Button();
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
            // ratingTextBox
            // 
            this.ratingTextBox.Location = new System.Drawing.Point(471, 12);
            this.ratingTextBox.Name = "ratingTextBox";
            this.ratingTextBox.Size = new System.Drawing.Size(100, 20);
            this.ratingTextBox.TabIndex = 1;
            // 
            // barsComboBox
            // 
            this.barsComboBox.FormattingEnabled = true;
            this.barsComboBox.Location = new System.Drawing.Point(344, 12);
            this.barsComboBox.Name = "barsComboBox";
            this.barsComboBox.Size = new System.Drawing.Size(121, 21);
            this.barsComboBox.TabIndex = 2;
            // 
            // ratingButton
            // 
            this.ratingButton.Location = new System.Drawing.Point(577, 12);
            this.ratingButton.Name = "ratingButton";
            this.ratingButton.Size = new System.Drawing.Size(75, 23);
            this.ratingButton.TabIndex = 3;
            this.ratingButton.Text = "Rate";
            this.ratingButton.UseVisualStyleBackColor = true;
            this.ratingButton.Click += new System.EventHandler(this.ratingButton_Click);
            // 
            // BarRatingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1223, 595);
            this.Controls.Add(this.ratingButton);
            this.Controls.Add(this.barsComboBox);
            this.Controls.Add(this.ratingTextBox);
            this.Controls.Add(this.barRatingsDataGrid);
            this.Name = "BarRatingsForm";
            this.Text = "BarRatings";
            ((System.ComponentModel.ISupportInitialize)(this.barRatingsDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView barRatingsDataGrid;
        private System.Windows.Forms.TextBox ratingTextBox;
        private System.Windows.Forms.ComboBox barsComboBox;
        private System.Windows.Forms.Button ratingButton;
    }
}