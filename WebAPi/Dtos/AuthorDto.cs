using System.Collections.Generic;

namespace WebAPi.Dtos
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public List<BookDto> Books { get; set; }
    }
}
