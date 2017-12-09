﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.DataModels
{
    [Table("UserRatings")]
    public class UsersRatingToBar
    {
        public UsersRatingToBar(BarData bar, User user, int rating)
        {
            this.Bar = bar;
            this.User = user;
            this.Rating = rating;
        }
        public UsersRatingToBar() { }

        [ForeignKey("Bar"), Key, Column(Order = 0), MaxLength(50)]
        public string BarId { get; set; }
        public virtual BarData Bar { get; set; }

        [ForeignKey("User"), Key, Column(Order = 1), MaxLength(50)]
        public string Username { get; set; }
        public virtual User User { get; set; }

        [Required]
        public int Rating { get; set; }

        [MaxLength(400)]
        public string Comment { get; set; }
    }
}
