namespace WebApi.Models
{
    public class BarModel // New model for sending data through WebApi
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public float Rating { get; set; }
        public string Category { get; set; }
        public double Distance { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
    }
}