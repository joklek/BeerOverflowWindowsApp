using BeerOverflowWindowsApp.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace BeerOverflowWindowsApp
{
    class BarRating
    {
        BarDataModel barsData = null;

        public BarDataModel GetBarsData()
        {
            return barsData;
        }

        public BarRating()
        {
            barsData = BarFileReader.ReadData();
        }

        public void AddRating(BarData barData, int rating)
        {
            var barsCount = barsData.BarsList.Where(x => x.Title == barData.Title).Count();
            if (barsCount > 0)
            {
                var ratings = barsData.BarsList.Where(x => x.Title == barData.Title).Select(x => x.Ratings).FirstOrDefault();
                if(ratings == null) { ratings = new List<int> { }; }
                ratings.Add(rating);
                barsData.BarsList.Where(x => x.Title == barData.Title).Select(x => x.Ratings = ratings).ToList();
            }
            else
            {
                barData.Ratings = new List<int> { };
                barData.Ratings.Add(rating);
                barsData.BarsList.Add(barData);
            }
            BarFileWriter.SaveData(barsData);
        }

        public void AddBars(List<BarData> barsList)
        {
            foreach (var bar in barsList)
            {
                if (barsData.BarsList.Where(x => x.Title == bar.Title).Count() == 0)
                {
                    barsData.BarsList.Add( bar );
                }
            }
            BarFileWriter.SaveData(barsData);
        }
    }
}
