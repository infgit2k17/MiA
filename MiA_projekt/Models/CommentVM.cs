namespace MiA_projekt.Models
{
    public class CommentVM
    {
        public string AuthorName { get; set; }

        public string Comment { get; set; }
    }

    public class CommentVm2
    {
        public string Comment { get; set; }

        public int Rate { get; set; }

        public int ApartmentId { get; set; }
    }
}
