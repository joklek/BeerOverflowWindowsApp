using System.Collections.Generic;
using System.Linq;
using BarData = WebApi.DataModels.BarData;
using User = WebApi.DataModels.User;
using UsersRatingToBar = WebApi.DataModels.UsersRatingToBar;

namespace WebApi.Database
{
    public class DatabaseManager
    {
        public bool Register(string username, string password)
        {
            User currentUser = new User { Username = username, Password = password };
            using (var db = new BarsDatabase())
            {
                if (db.Users.FirstOrDefault<User>(user => user.Username == username) != null)
                {
                    return false;
                }
                db.Users.Add(currentUser);
                db.SaveChanges();
                return true;
            }
        }
        public void SaveBarRating(BarData barToRate, User currentUser, int rating)
        {
            using (var db = new BarsDatabase())
            {
                var userInDb = db.Users.FirstOrDefault(user => user.Username == currentUser.Username);
                if (userInDb == null)
                    userInDb = new User { Username = currentUser.Username };
                var barInDb = db.Bars.FirstOrDefault(bar => bar.BarId == barToRate.BarId);
                if (barInDb != null)
                {
                    var barRating = db.UserRatings.Find(barInDb.BarId, userInDb.Username);
                    if (barRating != null)  
                    {
                        barRating.Rating = rating;                       
                    }
                    else   
                    {
                        db.UserRatings.Add(new UsersRatingToBar(barInDb, userInDb, rating));
                    }
                    db.SaveChanges();
                    barInDb.AvgRating = (float)db.UserRatings.Where(x => x.BarId == barInDb.BarId).Select(x => x.Rating).DefaultIfEmpty().Average(); //
                }
                else  
                {
                    barToRate.AvgRating = rating;
                    db.SaveChanges();
                    db.UserRatings.Add(new UsersRatingToBar(barToRate, userInDb, rating));                  
                }
                db.SaveChanges();
            }
        }

        public List<BarData> GetAllBarData(List<BarData> localBars, User user)
        {
            using (var db = new BarsDatabase())
            {               
                localBars.ForEach(bar => 
                {
                    var barInDb = db.Bars.FirstOrDefault(dbBar => dbBar.BarId == bar.BarId);
                    bar.AvgRating = barInDb?.AvgRating ?? 0;
                    var barRating = db.UserRatings.FirstOrDefault(userRating => userRating.BarId == bar.BarId && userRating.Username == user.Username);
                    bar.UserRating = barRating?.Rating ?? 0;
                });
            }
            return localBars;
        }

        public BarData GetBarRatings(BarData bar, User user)
        {
            using (var db = new BarsDatabase())
            {
                var barInDb = db.Bars.FirstOrDefault(dbBar => dbBar.BarId == bar.BarId);
                bar.AvgRating = barInDb?.AvgRating ?? 0;
                var barRating = db.UserRatings.FirstOrDefault(userRating => userRating.BarId == bar.BarId && userRating.Username == user.Username);
                bar.UserRating = barRating?.Rating ?? 0;
            }
            return bar;
        }
    }
}