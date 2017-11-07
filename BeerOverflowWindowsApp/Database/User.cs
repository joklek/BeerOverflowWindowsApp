using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeerOverflowWindowsApp.Database
{
    [Table("User")]
    public class User
    {
        [Key]
        public String Username { get; set; }
        public String Password { get; set; }

        public virtual ICollection<UsersRatingToBar> UserRatings { get; set; }
    }
}
