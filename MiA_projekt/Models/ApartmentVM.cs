namespace MiA_projekt.Models
{
    public class ApartmentVM
    {
        public int Id { get; set; }
        
        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public double RatingStars { get; set; }

        public decimal Price { get; set; }
    }
}
