using System.ComponentModel.DataAnnotations;

namespace WebAPi.Entities
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(70, ErrorMessage = "Title length maximum character should be 70")]
        public string Title { get; set; }
        public int PageNumber { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
