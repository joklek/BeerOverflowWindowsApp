using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.DataModels
{
    [Table("Bar")]
    public class BarData : IEquatable<BarData>
    {
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

        public string StreetAddress { get; set; }

        public string City { get; set; }

        public CategoryTypes Categories { get; set; }

        public virtual ICollection<UsersRatingToBar> UserRatings { get; set; }
        public virtual ICollection<BarSynonym> Synonyms { get; set; }

        [NotMapped]
        public double DistanceToCurrentLocation { get; set; }

        // Equals and GetHasCode are auto generated
        public bool Equals(BarData y)
        {
            if (ReferenceEquals(this, y)) return true;
            if (ReferenceEquals(this, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (this.GetType() != y.GetType()) return false;
            return string.Equals(this.Title, y.Title) && 
                   string.Equals(this.BarId, y.BarId) && 
                   this.AvgRating.Equals(y.AvgRating) && 
                   this.UserRating == y.UserRating && 
                   this.Latitude.Equals(y.Latitude) && 
                   this.Longitude.Equals(y.Longitude) && 
                   string.Equals(this.StreetAddress, y.StreetAddress) && 
                   string.Equals(this.City, y.City) && 
                   this.Categories == y.Categories && 
                   Equals(this.UserRatings, y.UserRatings) && 
                   Equals(this.Synonyms, y.Synonyms) && 
                   this.DistanceToCurrentLocation.Equals(y.DistanceToCurrentLocation);
        }

        public int GetHashCode(BarData obj)
        {
            unchecked
            {
                var hashCode = (obj.Title != null ? obj.Title.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (obj.BarId != null ? obj.BarId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ obj.AvgRating.GetHashCode();
                hashCode = (hashCode * 397) ^ obj.UserRating;
                hashCode = (hashCode * 397) ^ obj.Latitude.GetHashCode();
                hashCode = (hashCode * 397) ^ obj.Longitude.GetHashCode();
                hashCode = (hashCode * 397) ^ (obj.StreetAddress != null ? obj.StreetAddress.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (obj.City != null ? obj.City.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) obj.Categories;
                hashCode = (hashCode * 397) ^ (obj.UserRatings != null ? obj.UserRatings.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (obj.Synonyms != null ? obj.Synonyms.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ obj.DistanceToCurrentLocation.GetHashCode();
                return hashCode;
            }
        }
    }
}
