using System.ComponentModel.DataAnnotations;

namespace LibraryMVC.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string Blurb { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public int? GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}