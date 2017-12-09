using System.Collections.Generic;

namespace WebApi.DataModels
{
    public class UserAndBarsModel
    {
        public User User { get; set; }
        public List<string> BarIds { get; set; }
    }
}
