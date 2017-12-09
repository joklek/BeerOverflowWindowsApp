namespace WebApi.DataModels
{
    public class RatingModel
    {
        public string BarID { get; set; }
        public int Rating { get; set; }
        public User User { get; set; }
    }
}
