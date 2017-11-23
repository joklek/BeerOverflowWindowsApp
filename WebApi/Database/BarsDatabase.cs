﻿using System.Data.Entity;
using BeerOverflowWindowsApp.DataModels;

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
