using BeerOverflowWindowsApp.BarComparers;
using BeerOverflowWindowsApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerOverflowWindowsApp
{
    public class DatabaseManager
    {
        static BarsDatabase db = new BarsDatabase();
        public void SaveData(BarDataModel barData)
        {
           // using (var db = new BarsDatabase())
            {
                /*
                var barNames = db.Bars.Select(x => x.Title);

                foreach (var bar in barData)
                {
                    if(!barNames.Contains(bar.Title))
                        db.Bars.Add(bar);
                }             
                */

                foreach (var bar in barData)
                {
                    var result = db.Bars.SingleOrDefault(b => b.Title == bar.Title);
                    if (result != null)
                    {
                        result.Ratings = bar.Ratings;
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Bars.Add(bar);
                        db.SaveChanges();
                    }

                    /*
                                        BarData barInDb;
                                        if((barInDb = db.Bars.Find(bar.Title)) != null)
                                        {
                                            barInDb.Ratings = bar.Ratings;
                                        } else
                                        {
                                            db.Bars.Add(bar);
                                        }
                    */
                }
                //db.SaveChanges();
               // RefreshAll(db);
               // var a = db.ChangeTracker.Entries();
                //db.Entry(Bars).State = EntityState.Detached;
  /*              var query = from b in db.Bars
                            orderby b.Title
                            select b;

                Console.WriteLine("All bars in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Title);
                }  //db.Database.Delete();*/
            }
        }

        public BarDataModel GetAllBarData()
        {
            BarDataModel barsData = new BarDataModel();
           // using (var db = new BarsDatabase())
            {              
                barsData.AddRange(db.Bars);              
            }
            return barsData;
        }

        public List<int> GetBarRatings(BarData bar)
        {
            List<int> ratings;
           // using (var db = new BarsDatabase())
            {
                ratings = db.Bars.FirstOrDefault(x => x.Title == bar.Title)?.Ratings ?? new List<int>();
            }
            return ratings;
        }

        public void RefreshAll(BarsDatabase db)
        {
            foreach (var entity in db.ChangeTracker.Entries())
            {
                //entity.Reload();
                db.Entry(entity).State = EntityState.Detached;
            }
        }

    }
}
