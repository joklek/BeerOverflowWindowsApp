﻿using BeerOverflowWindowsApp.BarComparers;
using BeerOverflowWindowsApp.DataModels;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace BeerOverflowWindowsApp
{
    public partial class BarRatingsForm : Form
    {
        private const string titleColumnName = "Title";
        private const string ratingColumnName = "Rating";
        BarRating _barRating = new BarRating();

        public BarRatingsForm()
        {
            InitializeComponent();
            barRatingsDataGrid.Columns.Add("titleColumn", titleColumnName);
            barRatingsDataGrid.Columns.Add("ratingColumn", ratingColumnName);
            ReLoadForm();
        }

        public void ReLoadForm()
        {            
            var barData = _barRating.GetBarsData();
            barRatingsDataGrid.Rows.Clear();
            barsComboBox.DataSource = barData.BarsList.Select(x => x.Title).ToList();
            foreach (var bar in barData.BarsList)
            {
                var rating = bar.Ratings?.Average().ToString(CultureInfo.InvariantCulture) ?? "-";
                barRatingsDataGrid.Rows.Add(bar.Title, rating);
            }
        }

        private void RatingButton_Click(object sender, EventArgs e)
        {
            var rating = manualBarRating.Rating;
            if (barsComboBox.SelectedIndex != -1 && rating != "" && int.TryParse(rating, out var ratingNumber))
            {
                var barData = new BarData { Title = barsComboBox.SelectedItem.ToString() };
                _barRating.AddRating(barData, ratingNumber);
                ReLoadForm();
            }
        }

        private void ButtonSortByTitle_Click(object sender, EventArgs e)
        {
            _barRating.Sort(CompareType.Title);            
            ReLoadForm();
        }

        private void ButtonSortByRating_Click(object sender, EventArgs e)
        {
            _barRating.Sort(CompareType.Rating);
            ReLoadForm();
        }

        private void ButtonSortByDistance_Click(object sender, EventArgs e)
        {
            _barRating.Sort(CompareType.Distance);
            ReLoadForm();
        }

        private void BarRatingsDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                barsComboBox.SelectedIndex = e.RowIndex;
            }            
        }
    }
}
