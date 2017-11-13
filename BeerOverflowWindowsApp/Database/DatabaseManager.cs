using BeerOverflowWindowsApp.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace BeerOverflowWindowsApp.Database
{
    public static class DatabaseManager
    {
        private static readonly BarsDatabase _db = new BarsDatabase();
        public static void SaveBarRating(BarData barToRate, int rating)
        {          
            var userInDb = _db.Users.FirstOrDefault(user => user.Username == Program.defaultUser.Username);
            if (userInDb == null)
                userInDb = new User { Username = Program.defaultUser.Username };
            var barInDb = _db.Bars.FirstOrDefault(bar => bar.BarId == barToRate.BarId);
            if (barInDb != null)
            {
                var barRating = _db.UserRatings.Find(barInDb.BarId, userInDb.Username);
                if (barRating != null)
                {
                    barRating.Rating = rating;
                }
                else
                {
                    _db.UserRatings.Add(new UsersRatingToBar(barInDb, userInDb, rating));
                }
            }
            else
            {
                _db.UserRatings.Add(new UsersRatingToBar(barToRate, userInDb, rating));
            }
            _db.SaveChanges();              
        }

        public static BarDataModel GetAllBarData(BarDataModel localBars)
        {
            localBars.ForEach(bar => bar.Ratings = _db.UserRatings.Where(x => x.BarId == bar.BarId).Select(x => x.Rating).ToList());
            return localBars;
        }

        public static List<int> GetBarRatings(BarData bar)
        {
            var list = _db.UserRatings.Where(x => x.BarId == bar.BarId).Select(x => x.Rating).ToList();
            return list;
        }
    }
}
