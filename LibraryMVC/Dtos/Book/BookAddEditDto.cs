using System.ComponentModel.DataAnnotations;

namespace LibraryMVC.Dtos.Books
{
    public class BookAddEditDto
    {
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        public string Blurb { get; set; }

        [Required]
        public int AuthorId { get; set; }

        public int GenreId { get; set; }
    }
}