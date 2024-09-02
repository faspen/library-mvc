using LibraryMVC.Dtos.Book;

namespace LibraryMVC.Dtos.Author
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<BookDto> Books { get; set; }
    }
}