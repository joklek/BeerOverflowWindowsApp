using BeerOverflowWindowsApp.DataModels;
using System;
using System.Linq;
using System.Windows.Forms;

namespace BeerOverflowWindowsApp
{
    public partial class BarRatingsForm : Form
    {
        private const string titleColumnName = "Title";
        private const string ratingColumnName = "Rating";

        BarRating barRating = new BarRating();

        public BarRatingsForm()
        {
            InitializeComponent();
            barRatingsDataGrid.Columns.Add("titleColumn", titleColumnName);
            barRatingsDataGrid.Columns.Add("ratingColumn", ratingColumnName);
            ReLoadForm();
        }

        public void ReLoadForm()
        {            
            var barData = barRating.GetBarsData();
            barRatingsDataGrid.Rows.Clear();
            foreach (var bar in barData.BarsList)
            {
                var rating = bar.Ratings == null ? "-"  : bar.Ratings.Average().ToString();
                barRatingsDataGrid.Rows.Add(bar.Title, rating);
                barsComboBox.Items.Add(bar.Title);
            }
        }

        private void RatingButton_Click(object sender, EventArgs e)
        {
            var rating = BeerPanel.Rating;
            if (barsComboBox.SelectedIndex != -1 && rating != "" && int.TryParse(rating, out int ratingNumber))
            {
                var barData = new BarData { Title = barsComboBox.SelectedItem.ToString() };
                barRating.AddRating(barData, ratingNumber);
                ReLoadForm();
            }
        }
    }
}
