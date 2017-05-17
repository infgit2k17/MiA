namespace MiA_projekt.Models
{
    public class ApartmentViewModel
    {
        public int id { get; set; }
        
        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public double RatingStars { get; set; }

        public decimal Price { get; set; }
    }
}
