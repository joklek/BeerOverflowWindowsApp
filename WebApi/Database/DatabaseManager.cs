using System.Collections.Generic;
using System.Linq;
using WebApi.DataModels;

namespace WebApi.Database
{
    public class DatabaseManager
    {

        public void SaveBars(List<BarData> barsToSave)
        {
            using (var db = new BarsDatabase())
            {
                barsToSave.ForEach(barToSave =>
                {
                    if (db.Bars.FirstOrDefault<BarData>(barInDB => barInDB.BarId == barToSave.BarId) == null)
                    {
                        db.Bars.Add(barToSave);
                        db.SaveChanges();
                    }
                });
            }
        }

        public bool Register(User currentUser)
        {         
            using (var db = new BarsDatabase())
            {
                if (db.Users.FirstOrDefault<User>(user => user.Username == currentUser.Username) != null)
                {
                    return false;
                }
                db.Users.Add(currentUser);
                db.SaveChanges();
                return true;
            }
        }

        public bool LogIn(User User)
        {
            using (var db = new BarsDatabase())
            {
                if (db.Users.FirstOrDefault<User>(user => user.Username == User.Username && user.Password == User.Password) != null)
                    return true;
            }
            return false;
        }
        public void SaveBarRating(string BarID, string currentUser, int rating)
        {
            using (var db = new BarsDatabase())
            {
                var userInDb = db.Users.FirstOrDefault(user => user.Username == currentUser);
                var barInDb = db.Bars.FirstOrDefault(bar => bar.BarId == BarID);
                if (barInDb != null)
                {
                    var barRating = barInDb.UserRatings.FirstOrDefault(userRating => userInDb.Username == userRating.Username);
                    if (barRating != null)
                    {
                        barRating.Rating = rating;
                    }
                    else
                    {
                        db.UserRatings.Add(new UsersRatingToBar(barInDb, userInDb, rating));
                    }
                    db.SaveChanges();
                    barInDb.AvgRating = (float)barInDb.UserRatings.Select(x => x.Rating).DefaultIfEmpty().Average();
                }
                db.SaveChanges();
            }
        }

        public List<BarData> GetAllBarData(List<BarData> localBars, string username)
        {
            using (var db = new BarsDatabase())
            {
                localBars.ForEach(bar =>
                {
                    var barInDb = db.Bars.FirstOrDefault(dbBar => dbBar.BarId == bar.BarId);
                    bar.AvgRating = barInDb?.AvgRating ?? 0;
                    var barRating = db.UserRatings.FirstOrDefault(userRating => userRating.BarId == bar.BarId && userRating.Username == username);
                    bar.UserRating = barRating?.Rating ?? 0;
                });
            }
            return localBars;
        }

        public BarData GetBarRatings(BarData bar, string user)
        {
            using (var db = new BarsDatabase())
            {
                var barInDb = db.Bars.FirstOrDefault(dbBar => dbBar.BarId == bar.BarId);
                bar.AvgRating = barInDb?.AvgRating ?? 0;
                var barRating = db.UserRatings.FirstOrDefault(userRating => userRating.BarId == bar.BarId && userRating.Username == user);
                bar.UserRating = barRating?.Rating ?? 0;
            }
            return bar;
        }
    }
}