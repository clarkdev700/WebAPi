using System.ComponentModel.DataAnnotations;

namespace WebAPi.Dtos
{
    public class BookCreationDto
    {
        [Required]
        [MaxLength(70, ErrorMessage = "Title length maximum character should be 70")]
        public string Title { get; set; }
        public int PageNumber { get; set; }
        public int AuthorId { get; set; }
    }
}
