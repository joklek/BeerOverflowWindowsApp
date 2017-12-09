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
                    if (db.Bars.FirstOrDefault(barInDb => barInDb.BarId == barToSave.BarId) != null) return;
                    db.Bars.Add(barToSave);
                    db.SaveChanges();
                });
            }
        }

        public bool UserCanVote(string barId, User currentUser)
        {
            using (var db = new BarsDatabase())
            {
                var userInDb = db.Users.FirstOrDefault(user => user.Username == currentUser.Username);
                if (userInDb == null)
                {
                    // Not sure if right behavior? Maybe reject request and ask for registration?
                    // TODO: User registration
                    userInDb = new User { Username = currentUser.Username };
                    db.Users.Add(userInDb);
                    db.SaveChanges();
                }

                var timeNow = DateTime.Now;
                if (!TimeSpan.TryParse(ratingCooldownPeriod, out var cooldownTimeSpan))
                {
                    var badCooldown = ratingCooldownPeriod ?? "null";
                    throw new InvalidOperationException("Cofiguration file not present or incorrect value of variable "
                                                        + nameof(ratingCooldownPeriod) + " : " + badCooldown);
                }
                var recentRatingByUser = db.UserRatings.FirstOrDefault(x => x.BarId == barId &&
                                                                            x.Username == currentUser.Username &&
                                                                            x.RatingDate.Add(cooldownTimeSpan) < timeNow);
                return recentRatingByUser == null;
            }
        }


        public bool Register(string username, string password)
        {
            using (var db = new BarsDatabase())
            {
                if (db.Users.FirstOrDefault(user => user.Username == username) != null)
                {
                    return false;
                }
                var currentUser = new User { Username = username, Password = password };
                db.Users.Add(currentUser);
                db.SaveChanges();
                return true;
            }
        }

        public void SaveBarRating(string barId, User currentUser, int rating)
        {
            using (var db = new BarsDatabase())
            {
                var userInDb = db.Users.FirstOrDefault(user => user.Username == currentUser.Username);
                if (userInDb == null)
                {
                    // Not sure if right behavior? Maybe reject request and ask for registration?
                    // TODO: User registration
                    userInDb = new User { Username = currentUser.Username };
                    db.Users.Add(userInDb);
                }

                var barInDb = db.Bars.FirstOrDefault(bar => bar.BarId == barId);
                if (barInDb == null)
                {
                    Console.Write("This should not happen. Asking to rate non-existent bar");
                    return;
                }
                db.UserRatings.Add(new UsersRatingToBar(barInDb, userInDb, rating, DateTime.Now));
                barInDb.AvgRating = (float)db.UserRatings.Where(x => x.BarId == barInDb.BarId).Select(x => x.Rating).DefaultIfEmpty().Average();

                db.SaveChanges();
            }
        }

        public List<BarData> GetAllBarData(IEnumerable<string> localBarIds, string username)
        {
            var localBars = new List<BarData>();
            using (var db = new BarsDatabase())
            {
                localBars.AddRange(localBarIds.Select(localBarId => db.Bars.FirstOrDefault(dbBar => dbBar.BarId == localBarId)).Where(localBar => localBar != null));
            }
            return localBars;
        }

        /*public BarData GetBarRatings(string barId, User user)
        {
            var bar = new BarData();
            using (var db = new BarsDatabase())
            {
                var barInDb = db.Bars.FirstOrDefault(dbBar => dbBar.BarId == barId);
                bar.AvgRating = barInDb?.AvgRating ?? 0;
                var barRating = db.UserRatings.FirstOrDefault(userRating => userRating.BarId == bar.BarId && userRating.Username == user.Username);
                bar.UserRating = barRating?.Rating ?? 0;
            }
            return bar;
        }*/
    }
}