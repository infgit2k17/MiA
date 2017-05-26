using System.Collections.Generic;

namespace MiA_projekt.Models
{
    public class ApartmentDetailsVM
    {
        public int Id { get; set; }
        
        public string Title { get; set; }

        public string Description { get; set; }

        public IEnumerable<string> Images { get; set; }

        public double RatingStars { get; set; }

        public decimal Price { get; set; }

        public IEnumerable<CommentVM> Comments { get; set; }
    }
}
