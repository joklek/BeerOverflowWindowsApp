using BeerOverflowWindowsApp.BarRaters;

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
            this.PanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.BarDataGridView = new System.Windows.Forms.DataGridView();
            this.PanelOptions = new System.Windows.Forms.TableLayoutPanel();
            this.LatitudeLabel = new System.Windows.Forms.Label();
            this.LatitudeTextBox = new System.Windows.Forms.TextBox();
            this.LongitudeTextBox = new System.Windows.Forms.TextBox();
            this.LongitudeLabel = new System.Windows.Forms.Label();
            this.RadiusLabel = new System.Windows.Forms.Label();
            this.GoButton = new System.Windows.Forms.Button();
            this.RadiusTextBox = new System.Windows.Forms.TextBox();
            this.MapButton = new System.Windows.Forms.Button();
            this.PanelFilter = new System.Windows.Forms.TableLayoutPanel();
            this.FilterAll = new System.Windows.Forms.CheckBox();
            this.FilterBar = new System.Windows.Forms.CheckBox();
            this.FilterRestaurant = new System.Windows.Forms.CheckBox();
            this.FilterClub = new System.Windows.Forms.CheckBox();
            this.FilterEmpty = new System.Windows.Forms.CheckBox();
            this.manualBarRating = new BeerOverflowWindowsApp.BarRaters.ManualBarRating();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rating = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Distance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BarDataGridView)).BeginInit();
            this.PanelOptions.SuspendLayout();
            this.PanelFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelMain
            // 
            this.PanelMain.AutoSize = true;
            this.PanelMain.ColumnCount = 2;
            this.PanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.PanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.PanelMain.Controls.Add(this.BarDataGridView, 1, 0);
            this.PanelMain.Controls.Add(this.PanelOptions, 0, 0);
            this.PanelMain.Controls.Add(this.manualBarRating, 1, 2);
            this.PanelMain.Controls.Add(this.progressBar, 1, 1);
            this.PanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelMain.Location = new System.Drawing.Point(0, 0);
            this.PanelMain.Name = "PanelMain";
            this.PanelMain.RowCount = 3;
            this.PanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.PanelMain.Size = new System.Drawing.Size(717, 410);
            this.PanelMain.TabIndex = 13;
            // 
            // BarDataGridView
            // 
            this.BarDataGridView.AllowUserToAddRows = false;
            this.BarDataGridView.AllowUserToDeleteRows = false;
            this.BarDataGridView.AllowUserToResizeColumns = false;
            this.BarDataGridView.AllowUserToResizeRows = false;
            this.BarDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BarDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Title,
            this.Category,
            this.Rating,
            this.Distance});
            this.BarDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BarDataGridView.Location = new System.Drawing.Point(207, 3);
            this.BarDataGridView.MultiSelect = false;
            this.BarDataGridView.Name = "BarDataGridView";
            this.BarDataGridView.ReadOnly = true;
            this.BarDataGridView.RowHeadersVisible = false;
            this.BarDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.BarDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.BarDataGridView.Size = new System.Drawing.Size(520, 265);
            this.BarDataGridView.TabIndex = 15;
            this.BarDataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.BarDataGridView_ColumnHeaderMouseClick);
            this.BarDataGridView.SelectionChanged += new System.EventHandler(this.BarDataGridView_SelectionChanged);
            // 
            // PanelOptions
            // 
            this.PanelOptions.AutoSize = true;
            this.PanelOptions.ColumnCount = 2;
            this.PanelOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PanelOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 138F));
            this.PanelOptions.Controls.Add(this.LatitudeLabel, 0, 0);
            this.PanelOptions.Controls.Add(this.LatitudeTextBox, 1, 0);
            this.PanelOptions.Controls.Add(this.LongitudeTextBox, 1, 1);
            this.PanelOptions.Controls.Add(this.LongitudeLabel, 0, 1);
            this.PanelOptions.Controls.Add(this.RadiusLabel, 0, 2);
            this.PanelOptions.Controls.Add(this.GoButton, 1, 3);
            this.PanelOptions.Controls.Add(this.RadiusTextBox, 1, 2);
            this.PanelOptions.Controls.Add(this.MapButton, 1, 4);
            this.PanelOptions.Controls.Add(this.PanelFilter, 1, 5);
            this.PanelOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelOptions.Location = new System.Drawing.Point(3, 3);
            this.PanelOptions.Name = "PanelOptions";
            this.PanelOptions.RowCount = 6;
            this.PanelOptions.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PanelOptions.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PanelOptions.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PanelOptions.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PanelOptions.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PanelOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.PanelOptions.Size = new System.Drawing.Size(198, 265);
            this.PanelOptions.TabIndex = 13;
            // 
            // LatitudeLabel
            // 
            this.LatitudeLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LatitudeLabel.AutoSize = true;
            this.LatitudeLabel.Location = new System.Drawing.Point(3, 6);
            this.LatitudeLabel.Name = "LatitudeLabel";
            this.LatitudeLabel.Size = new System.Drawing.Size(45, 13);
            this.LatitudeLabel.TabIndex = 0;
            this.LatitudeLabel.Text = "Latitude";
            // 
            // LatitudeTextBox
            // 
            this.LatitudeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LatitudeTextBox.Location = new System.Drawing.Point(63, 3);
            this.LatitudeTextBox.Name = "LatitudeTextBox";
            this.LatitudeTextBox.Size = new System.Drawing.Size(132, 20);
            this.LatitudeTextBox.TabIndex = 3;
            this.LatitudeTextBox.TextChanged += new System.EventHandler(this.LatitudeTextBox_TextChanged);
            // 
            // LongitudeTextBox
            // 
            this.LongitudeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LongitudeTextBox.Location = new System.Drawing.Point(63, 29);
            this.LongitudeTextBox.Name = "LongitudeTextBox";
            this.LongitudeTextBox.Size = new System.Drawing.Size(132, 20);
            this.LongitudeTextBox.TabIndex = 4;
            this.LongitudeTextBox.TextChanged += new System.EventHandler(this.LongitudeTextBox_TextChanged);
            // 
            // LongitudeLabel
            // 
            this.LongitudeLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LongitudeLabel.AutoSize = true;
            this.LongitudeLabel.Location = new System.Drawing.Point(3, 32);
            this.LongitudeLabel.Name = "LongitudeLabel";
            this.LongitudeLabel.Size = new System.Drawing.Size(54, 13);
            this.LongitudeLabel.TabIndex = 1;
            this.LongitudeLabel.Text = "Longitude";
            // 
            // RadiusLabel
            // 
            this.RadiusLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.RadiusLabel.AutoSize = true;
            this.RadiusLabel.Location = new System.Drawing.Point(3, 58);
            this.RadiusLabel.Name = "RadiusLabel";
            this.RadiusLabel.Size = new System.Drawing.Size(40, 13);
            this.RadiusLabel.TabIndex = 2;
            this.RadiusLabel.Text = "Radius";
            // 
            // GoButton
            // 
            this.GoButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GoButton.Location = new System.Drawing.Point(63, 81);
            this.GoButton.Name = "GoButton";
            this.GoButton.Size = new System.Drawing.Size(132, 23);
            this.GoButton.TabIndex = 6;
            this.GoButton.Text = "Go";
            this.GoButton.UseVisualStyleBackColor = true;
            this.GoButton.Click += new System.EventHandler(this.GoButton_Click);
            // 
            // RadiusTextBox
            // 
            this.RadiusTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RadiusTextBox.Location = new System.Drawing.Point(63, 55);
            this.RadiusTextBox.Name = "RadiusTextBox";
            this.RadiusTextBox.Size = new System.Drawing.Size(132, 20);
            this.RadiusTextBox.TabIndex = 5;
            this.RadiusTextBox.TextChanged += new System.EventHandler(this.RadiusTextBox_TextChanged);
            // 
            // MapButton
            // 
            this.MapButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MapButton.Location = new System.Drawing.Point(63, 110);
            this.MapButton.Name = "MapButton";
            this.MapButton.Size = new System.Drawing.Size(132, 23);
            this.MapButton.TabIndex = 7;
            this.MapButton.Text = "Show Map";
            this.MapButton.UseVisualStyleBackColor = true;
            this.MapButton.Click += new System.EventHandler(this.MapButton_Click);
            // 
            // PanelFilter
            // 
            this.PanelFilter.ColumnCount = 1;
            this.PanelFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PanelFilter.Controls.Add(this.FilterAll, 0, 0);
            this.PanelFilter.Controls.Add(this.FilterBar, 0, 1);
            this.PanelFilter.Controls.Add(this.FilterRestaurant, 0, 2);
            this.PanelFilter.Controls.Add(this.FilterClub, 0, 3);
            this.PanelFilter.Controls.Add(this.FilterEmpty, 0, 4);
            this.PanelFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelFilter.Location = new System.Drawing.Point(63, 139);
            this.PanelFilter.Name = "PanelFilter";
            this.PanelFilter.RowCount = 5;
            this.PanelFilter.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PanelFilter.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PanelFilter.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PanelFilter.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PanelFilter.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PanelFilter.Size = new System.Drawing.Size(132, 123);
            this.PanelFilter.TabIndex = 8;
            // 
            // FilterAll
            // 
            this.FilterAll.AutoSize = true;
            this.FilterAll.Location = new System.Drawing.Point(3, 3);
            this.FilterAll.Name = "FilterAll";
            this.FilterAll.Size = new System.Drawing.Size(67, 17);
            this.FilterAll.TabIndex = 0;
            this.FilterAll.Text = "Show All";
            this.FilterAll.UseVisualStyleBackColor = true;
            this.FilterAll.Click += new System.EventHandler(this.FilterAll_Click);
            // 
            // FilterBar
            // 
            this.FilterBar.AutoSize = true;
            this.FilterBar.Location = new System.Drawing.Point(3, 26);
            this.FilterBar.Name = "FilterBar";
            this.FilterBar.Size = new System.Drawing.Size(72, 17);
            this.FilterBar.TabIndex = 1;
            this.FilterBar.Text = "Show Bar";
            this.FilterBar.UseVisualStyleBackColor = true;
            this.FilterBar.Click += new System.EventHandler(this.FilterBar_Click);
            // 
            // FilterRestaurant
            // 
            this.FilterRestaurant.AutoSize = true;
            this.FilterRestaurant.Location = new System.Drawing.Point(3, 49);
            this.FilterRestaurant.Name = "FilterRestaurant";
            this.FilterRestaurant.Size = new System.Drawing.Size(108, 17);
            this.FilterRestaurant.TabIndex = 2;
            this.FilterRestaurant.Text = "Show Restaurant";
            this.FilterRestaurant.UseVisualStyleBackColor = true;
            this.FilterRestaurant.Click += new System.EventHandler(this.FilterRestaurant_Click);
            // 
            // FilterClub
            // 
            this.FilterClub.AutoSize = true;
            this.FilterClub.Location = new System.Drawing.Point(3, 72);
            this.FilterClub.Name = "FilterClub";
            this.FilterClub.Size = new System.Drawing.Size(77, 17);
            this.FilterClub.TabIndex = 3;
            this.FilterClub.Text = "Show Club";
            this.FilterClub.UseVisualStyleBackColor = true;
            this.FilterClub.Click += new System.EventHandler(this.FilterClub_Click);
            // 
            // FilterEmpty
            // 
            this.FilterEmpty.AutoSize = true;
            this.FilterEmpty.Location = new System.Drawing.Point(3, 95);
            this.FilterEmpty.Name = "FilterEmpty";
            this.FilterEmpty.Size = new System.Drawing.Size(125, 17);
            this.FilterEmpty.TabIndex = 4;
            this.FilterEmpty.Text = "Show Uncategorized";
            this.FilterEmpty.UseVisualStyleBackColor = true;
            this.FilterEmpty.Click += new System.EventHandler(this.FilterEmpty_Click);
            // 
            // manualBarRating
            // 
            this.manualBarRating.ImageSize = 100;
            this.manualBarRating.Location = new System.Drawing.Point(207, 303);
            this.manualBarRating.MaximumSize = new System.Drawing.Size(500, 100);
            this.manualBarRating.MinimumSize = new System.Drawing.Size(500, 100);
            this.manualBarRating.Name = "manualBarRating";
            this.manualBarRating.Size = new System.Drawing.Size(500, 100);
            this.manualBarRating.TabIndex = 12;
            this.manualBarRating.Click += new System.EventHandler(this.ManualBarRating_Click);
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressBar.Location = new System.Drawing.Point(207, 274);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(520, 23);
            this.progressBar.TabIndex = 14;
            // 
            // Title
            // 
            this.Title.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Title.FillWeight = 9F;
            this.Title.HeaderText = "Title";
            this.Title.Name = "Title";
            this.Title.ReadOnly = true;
            this.Title.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Title.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Category
            // 
            this.Category.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Category.FillWeight = 5F;
            this.Category.HeaderText = "Category";
            this.Category.Name = "Category";
            this.Category.ReadOnly = true;
            this.Category.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Rating
            // 
            this.Rating.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Rating.FillWeight = 4F;
            this.Rating.HeaderText = "Rating";
            this.Rating.Name = "Rating";
            this.Rating.ReadOnly = true;
            this.Rating.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Rating.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Rating.Width = 63;
            // 
            // Distance
            // 
            this.Distance.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Distance.FillWeight = 5F;
            this.Distance.HeaderText = "Distance, (m)";
            this.Distance.Name = "Distance";
            this.Distance.ReadOnly = true;
            this.Distance.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Distance.Width = 94;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(717, 410);
            this.Controls.Add(this.PanelMain);
            this.Name = "MainWindow";
            this.Text = "BeerOverflow";
            this.PanelMain.ResumeLayout(false);
            this.PanelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BarDataGridView)).EndInit();
            this.PanelOptions.ResumeLayout(false);
            this.PanelOptions.PerformLayout();
            this.PanelFilter.ResumeLayout(false);
            this.PanelFilter.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ManualBarRating manualBarRating;
        private System.Windows.Forms.TableLayoutPanel PanelMain;
        private System.Windows.Forms.DataGridView BarDataGridView;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.TableLayoutPanel PanelOptions;
        private System.Windows.Forms.Label LatitudeLabel;
        private System.Windows.Forms.TextBox LatitudeTextBox;
        private System.Windows.Forms.TextBox LongitudeTextBox;
        private System.Windows.Forms.Label LongitudeLabel;
        private System.Windows.Forms.Label RadiusLabel;
        private System.Windows.Forms.Button GoButton;
        private System.Windows.Forms.TextBox RadiusTextBox;
        private System.Windows.Forms.Button MapButton;
        private System.Windows.Forms.TableLayoutPanel PanelFilter;
        private System.Windows.Forms.CheckBox FilterAll;
        private System.Windows.Forms.CheckBox FilterBar;
        private System.Windows.Forms.CheckBox FilterRestaurant;
        private System.Windows.Forms.CheckBox FilterClub;
        private System.Windows.Forms.CheckBox FilterEmpty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn Category;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rating;
        private System.Windows.Forms.DataGridViewTextBoxColumn Distance;
    }
}