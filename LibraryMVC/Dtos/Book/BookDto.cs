using LibraryMVC.Dtos.Authors;
using LibraryMVC.Dtos.Genres;

namespace LibraryMVC.Dtos.Books
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Blurb { get; set; }

        public int AuthorId { get; set; }
        public AuthorDto Author { get; set; }

        public int GenreId { get; set; }
        public GenreDto Genre { get; set; }
    }
}