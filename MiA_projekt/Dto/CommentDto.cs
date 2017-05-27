using System.ComponentModel.DataAnnotations;

namespace MiA_projekt.Dto
{
    public class CommentDto
    {
        [OnClick("readData(\" + item.id + \")", "item.id")]
        public int Id { get; set; }

        public int ApartmentId { get; set; }

        public string UserId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Text { get; set; }

    }
}
