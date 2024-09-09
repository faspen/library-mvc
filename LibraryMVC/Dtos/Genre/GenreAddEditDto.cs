using System.ComponentModel.DataAnnotations;

namespace LibraryMVC.Dtos.Genres
{
    public class GenreAddEditDto
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
    }
}