namespace BeerOverflowWindowsApp
{
    partial class MainWindow
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
            this.LongitudeLabel = new System.Windows.Forms.Label();
            this.LatitudeLabel = new System.Windows.Forms.Label();
            this.RadiusLabel = new System.Windows.Forms.Label();
            this.LongitudeTextBox = new System.Windows.Forms.TextBox();
            this.LatitudeTextBox = new System.Windows.Forms.TextBox();
            this.RadiusTextBox = new System.Windows.Forms.TextBox();
            this.GoButton = new System.Windows.Forms.Button();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.BarDataGridView = new System.Windows.Forms.DataGridView();
            this.ButtonSortByTitle = new System.Windows.Forms.Button();
            this.ButtonSortByRating = new System.Windows.Forms.Button();
            this.ButtonSortByDistance = new System.Windows.Forms.Button();
            this.manualBarRating = new BeerOverflowWindowsApp.ManualBarRating();
            this.titleColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ratingColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Distance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.BarDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // LongitudeLabel
            // 
            this.LongitudeLabel.AutoSize = true;
            this.LongitudeLabel.Location = new System.Drawing.Point(8, 43);
            this.LongitudeLabel.Name = "LongitudeLabel";
            this.LongitudeLabel.Size = new System.Drawing.Size(54, 13);
            this.LongitudeLabel.TabIndex = 0;
            this.LongitudeLabel.Text = "Longitude";
            // 
            // LatitudeLabel
            // 
            this.LatitudeLabel.AutoSize = true;
            this.LatitudeLabel.Location = new System.Drawing.Point(8, 15);
            this.LatitudeLabel.Name = "LatitudeLabel";
            this.LatitudeLabel.Size = new System.Drawing.Size(45, 13);
            this.LatitudeLabel.TabIndex = 1;
            this.LatitudeLabel.Text = "Latitude";
            // 
            // RadiusLabel
            // 
            this.RadiusLabel.AutoSize = true;
            this.RadiusLabel.Location = new System.Drawing.Point(8, 69);
            this.RadiusLabel.Name = "RadiusLabel";
            this.RadiusLabel.Size = new System.Drawing.Size(40, 13);
            this.RadiusLabel.TabIndex = 2;
            this.RadiusLabel.Text = "Radius";
            // 
            // LongitudeTextBox
            // 
            this.LongitudeTextBox.Location = new System.Drawing.Point(68, 40);
            this.LongitudeTextBox.Name = "LongitudeTextBox";
            this.LongitudeTextBox.Size = new System.Drawing.Size(125, 20);
            this.LongitudeTextBox.TabIndex = 3;
            this.LongitudeTextBox.TextChanged += new System.EventHandler(this.LongitudeTextBox_TextChanged);
            // 
            // LatitudeTextBox
            // 
            this.LatitudeTextBox.Location = new System.Drawing.Point(68, 12);
            this.LatitudeTextBox.Name = "LatitudeTextBox";
            this.LatitudeTextBox.Size = new System.Drawing.Size(125, 20);
            this.LatitudeTextBox.TabIndex = 4;
            this.LatitudeTextBox.TextChanged += new System.EventHandler(this.LatitudeTextBox_TextChanged);
            // 
            // RadiusTextBox
            // 
            this.RadiusTextBox.Location = new System.Drawing.Point(68, 66);
            this.RadiusTextBox.Name = "RadiusTextBox";
            this.RadiusTextBox.Size = new System.Drawing.Size(125, 20);
            this.RadiusTextBox.TabIndex = 5;
            this.RadiusTextBox.TextChanged += new System.EventHandler(this.RadiusTextBox_TextChanged);
            // 
            // GoButton
            // 
            this.GoButton.Location = new System.Drawing.Point(68, 92);
            this.GoButton.Name = "GoButton";
            this.GoButton.Size = new System.Drawing.Size(125, 23);
            this.GoButton.TabIndex = 6;
            this.GoButton.Text = "Go";
            this.GoButton.UseVisualStyleBackColor = true;
            this.GoButton.Click += new System.EventHandler(this.GoButton_Click);
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(211, 268);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(500, 23);
            this.ProgressBar.TabIndex = 7;
            this.ProgressBar.Visible = false;
            // 
            // BarDataGridView
            // 
            this.BarDataGridView.AllowUserToAddRows = false;
            this.BarDataGridView.AllowUserToDeleteRows = false;
            this.BarDataGridView.AllowUserToResizeColumns = false;
            this.BarDataGridView.AllowUserToResizeRows = false;
            this.BarDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BarDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.titleColumn,
            this.ratingColumn,
            this.Distance});
            this.BarDataGridView.Location = new System.Drawing.Point(211, 12);
            this.BarDataGridView.MultiSelect = false;
            this.BarDataGridView.Name = "BarDataGridView";
            this.BarDataGridView.ReadOnly = true;
            this.BarDataGridView.RowHeadersVisible = false;
            this.BarDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.BarDataGridView.Size = new System.Drawing.Size(500, 250);
            this.BarDataGridView.TabIndex = 8;
            this.BarDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.BarDataGridView_CellClick);
            this.BarDataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.BarDataGridView_ColumnHeaderMouseClick);
            // 
            // ButtonSortByTitle
            // 
            this.ButtonSortByTitle.Location = new System.Drawing.Point(717, 12);
            this.ButtonSortByTitle.Name = "ButtonSortByTitle";
            this.ButtonSortByTitle.Size = new System.Drawing.Size(96, 23);
            this.ButtonSortByTitle.TabIndex = 9;
            this.ButtonSortByTitle.Text = "Sort By Title";
            this.ButtonSortByTitle.UseVisualStyleBackColor = true;
            this.ButtonSortByTitle.Click += new System.EventHandler(this.ButtonSortByTitle_Click);
            // 
            // ButtonSortByRating
            // 
            this.ButtonSortByRating.Location = new System.Drawing.Point(717, 40);
            this.ButtonSortByRating.Name = "ButtonSortByRating";
            this.ButtonSortByRating.Size = new System.Drawing.Size(96, 23);
            this.ButtonSortByRating.TabIndex = 10;
            this.ButtonSortByRating.Text = "Sort By Rating";
            this.ButtonSortByRating.UseVisualStyleBackColor = true;
            this.ButtonSortByRating.Click += new System.EventHandler(this.ButtonSortByRating_Click);
            // 
            // ButtonSortByDistance
            // 
            this.ButtonSortByDistance.Location = new System.Drawing.Point(717, 69);
            this.ButtonSortByDistance.Name = "ButtonSortByDistance";
            this.ButtonSortByDistance.Size = new System.Drawing.Size(96, 23);
            this.ButtonSortByDistance.TabIndex = 11;
            this.ButtonSortByDistance.Text = "Sort By Distance";
            this.ButtonSortByDistance.UseVisualStyleBackColor = true;
            this.ButtonSortByDistance.Click += new System.EventHandler(this.ButtonSortByDistance_Click);
            // 
            // manualBarRating
            // 
            this.manualBarRating.ImageSize = 100;
            this.manualBarRating.Location = new System.Drawing.Point(211, 297);
            this.manualBarRating.MaximumSize = new System.Drawing.Size(500, 100);
            this.manualBarRating.MinimumSize = new System.Drawing.Size(500, 100);
            this.manualBarRating.Name = "manualBarRating";
            this.manualBarRating.Size = new System.Drawing.Size(500, 100);
            this.manualBarRating.TabIndex = 12;
            this.manualBarRating.Click += new System.EventHandler(this.manualBarRating_Click);
            // 
            // titleColumn
            // 
            this.titleColumn.FillWeight = 230F;
            this.titleColumn.HeaderText = "Title";
            this.titleColumn.Name = "titleColumn";
            this.titleColumn.ReadOnly = true;
            this.titleColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.titleColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.titleColumn.Width = 230;
            // 
            // ratingColumn
            // 
            this.ratingColumn.FillWeight = 125F;
            this.ratingColumn.HeaderText = "Rating";
            this.ratingColumn.Name = "ratingColumn";
            this.ratingColumn.ReadOnly = true;
            this.ratingColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ratingColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.ratingColumn.Width = 125;
            // 
            // Distance
            // 
            this.Distance.FillWeight = 125F;
            this.Distance.HeaderText = "Distance";
            this.Distance.Name = "Distance";
            this.Distance.ReadOnly = true;
            this.Distance.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Distance.Width = 125;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 410);
            this.Controls.Add(this.manualBarRating);
            this.Controls.Add(this.ButtonSortByDistance);
            this.Controls.Add(this.ButtonSortByRating);
            this.Controls.Add(this.ButtonSortByTitle);
            this.Controls.Add(this.BarDataGridView);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.GoButton);
            this.Controls.Add(this.RadiusTextBox);
            this.Controls.Add(this.LatitudeTextBox);
            this.Controls.Add(this.LongitudeTextBox);
            this.Controls.Add(this.RadiusLabel);
            this.Controls.Add(this.LatitudeLabel);
            this.Controls.Add(this.LongitudeLabel);
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            ((System.ComponentModel.ISupportInitialize)(this.BarDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LongitudeLabel;
        private System.Windows.Forms.Label LatitudeLabel;
        private System.Windows.Forms.Label RadiusLabel;
        private System.Windows.Forms.TextBox LongitudeTextBox;
        private System.Windows.Forms.TextBox LatitudeTextBox;
        private System.Windows.Forms.TextBox RadiusTextBox;
        private System.Windows.Forms.Button GoButton;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.DataGridView BarDataGridView;
        private System.Windows.Forms.Button ButtonSortByTitle;
        private System.Windows.Forms.Button ButtonSortByRating;
        private System.Windows.Forms.Button ButtonSortByDistance;
        private ManualBarRating manualBarRating;
        private System.Windows.Forms.DataGridViewTextBoxColumn titleColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ratingColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Distance;
    }
}