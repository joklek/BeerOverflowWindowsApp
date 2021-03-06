﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.DataModels
{
    [Table("Synonym")]
    public class BarSynonym
    {
        [Key, ForeignKey("Bar"), MaxLength(50), Column(Order = 0)]
        public string BarId { get; set; }

        public virtual BarData Bar { get; set; }

        [Key, MaxLength(50), Column(Order = 1)]
        public string Synonym { get; set; }
    }
}
