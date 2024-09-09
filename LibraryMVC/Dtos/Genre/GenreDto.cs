using LibraryMVC.Dtos.Books;

namespace LibraryMVC.Dtos.Genres
{
    public class GenreDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BookDto> Books { get; set; }
    }
}