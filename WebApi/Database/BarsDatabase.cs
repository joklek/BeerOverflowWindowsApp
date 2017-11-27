using System.Data.Entity;
using BarData = WebApi.DataModels.BarData;
using BarSynonym = WebApi.DataModels.BarSynonym;
using User = WebApi.DataModels.User;
using UsersRatingToBar = WebApi.DataModels.UsersRatingToBar;

namespace WebApi.Database
{
    public class BarsDatabase : DbContext
    {
        public DbSet<BarData> Bars { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UsersRatingToBar> UserRatings { get; set; }
        public DbSet<BarSynonym> Synonyms { get; set; }
    }
}
