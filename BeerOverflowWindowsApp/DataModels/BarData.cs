using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace BeerOverflowWindowsApp.DataModels
{
    public class BarData : IEquatable<BarData>
    {
        [Key]
        public string Title { get; set; }
        public string Id { get; set; }
        public List<int> Ratings { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public BarData()
        {
            Ratings = new List<int>();
        }

        [NotMapped]
        [JsonIgnore]
        public double DistanceToCurrentLocation { get; set; }

        public bool Equals(BarData other)
        {
            return this.Title == other.Title ? true : false;
        }
    }

}