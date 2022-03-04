using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPi.Entities
{
    public class Author
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Name Should be max of 50 characters")]
        [RegularExpression("^([a-zA-Z]{2,})[a-zA-Z\\s*]+$", ErrorMessage = "Numeric character not allowed!")]
        public string Name { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "LastName should be 50 characters")]
        [RegularExpression("^([a-zA-Z]{2,})[a-zA-Z\\s*]+$", ErrorMessage = "Numeric character not allowed!")]
        public string LastName { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
