using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using WebApi.DataModels;

namespace WebApi.Database
{
    public class DatabaseManager
    {
        private static readonly string ratingCooldownPeriod = ConfigurationManager.AppSettings["ratingCooldownPeriod"];

        public void SaveBars(List<BarData> barsToSave)
        {
            using (var db = new BarsDatabase())
            {
                barsToSave.ForEach(barToSave =>
                {
                    if (barToSave.BarId.Length > 50) { barToSave.BarId = new string(barToSave.BarId.Take(50).ToArray()); }
                    if (barToSave.Title.Length > 50) { barToSave.Title = new string(barToSave.Title.Take(50).ToArray()); }
                    if (db.Bars.FirstOrDefault(barInDb => barInDb.BarId == barToSave.BarId) != null) return;                   
                    db.Bars.Add(barToSave);
                    db.SaveChanges();
                });
            }
        }

        public bool UserCanVote(string barId, User currentUser, out TimeSpan cooldown)
        {
            using (var db = new BarsDatabase())
            {
                var userInDb = db.Users.FirstOrDefault(user => user.Username == currentUser.Username);
                if (userInDb == null || barId == null)
                {
                    cooldown = TimeSpan.Zero;
                    return false;
                }
                var timeNow = DateTime.Now;
                if (!TimeSpan.TryParse(ratingCooldownPeriod, out var cooldownTimeSpan))
                {
                    var badCooldown = ratingCooldownPeriod ?? "null";
                    throw new InvalidOperationException(string.Format("Cofiguration file not present or incorrect value of variable {0} : {1} ", 
                                                                      nameof(ratingCooldownPeriod),  badCooldown));
                }
                var cooldownTime = timeNow.Subtract(cooldownTimeSpan);
                var recentRatingByUser = db.UserRatings.FirstOrDefault(x => x.BarId == barId &&
                                                                       x.Username == currentUser.Username &&
                                                                       x.RatingDate.CompareTo(cooldownTime) >= 0);
                cooldown = recentRatingByUser?.RatingDate.Add(cooldownTimeSpan).Subtract(timeNow) ?? TimeSpan.Zero;
                return recentRatingByUser == null;
            }
        }

        public bool Register(User currentUser)
        {         
            using (var db = new BarsDatabase())
            {
                if (db.Users.FirstOrDefault(user => user.Username == currentUser.Username) != null)
                {
                    return false;
                }
                db.Users.Add(currentUser);
                db.SaveChanges();
                return true;
            }
        }

        public void SaveBarRating(string barId, User currentUser, int rating)
        {
            using (var db = new BarsDatabase())
            {
                if (!UserCanVote(barId, currentUser, out var cooldown)) return;
                var barInDb = db.Bars.FirstOrDefault(bar => bar.BarId == barId);
                if (barInDb == null)
                {
                    Console.Write("This should not happen. Asking to rate non-existent bar");
                    return;
                }
                db.UserRatings.Add(new UsersRatingToBar(barInDb, currentUser, rating, DateTime.Now));
                db.SaveChanges();
                var avgRating = (float) db.UserRatings.Where(x => x.BarId == barInDb.BarId).Select(x => x.Rating)
                    .DefaultIfEmpty().Average();
                barInDb.AvgRating = avgRating;
                db.SaveChanges();
            }
        }

        public bool LogIn(User userAttemptingToLogin)
        {
            using (var db = new BarsDatabase())
            {
                return db.Users.FirstOrDefault(user =>
                           user.Username == userAttemptingToLogin.Username &&
                           user.Password == userAttemptingToLogin.Password) != null;
            }
        }

        public List<BarData> GetAllBarData(IEnumerable<string> localBarIds)
        {
            var localBars = new List<BarData>();
            using (var db = new BarsDatabase())
            {
                localBars.AddRange(localBarIds.Select(localBarId => db.Bars.FirstOrDefault(dbBar => dbBar.BarId == localBarId)).Where(localBar => localBar != null));
            }
            return localBars;
        }

        public float GetBarRating(string barId)
        {
            float rating;
            using (var db = new BarsDatabase())
            {
                rating = db.Bars.FirstOrDefault(dbBar => dbBar.BarId == barId).AvgRating;
            }
            return rating;
        }
    }
}