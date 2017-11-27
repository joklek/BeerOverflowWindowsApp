using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.DataModels
{
    [Table("Bar")]
    public class BarData : IEquatable<BarData>
    {
        public BarData() { }

        [Required, MaxLength(50)]
        public string Title { get; set; }

        [Key, MaxLength(50)]
        public string BarId { get; set; }

        public float AvgRating { get; set; }

        [NotMapped]
        public int UserRating { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }
        public CategoryTypes Categories { get; set; }

        public virtual ICollection<UsersRatingToBar> UserRatings { get; set; }
        public virtual ICollection<BarSynonym> Synonyms { get; set; }

        [NotMapped]
        public double DistanceToCurrentLocation { get; set; }

        // TODO : Make IComparable
        public bool Equals(BarData other)
        {
            return other != null && (this.Title == other.Title ? true : false);
        }
    }
}
