using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryMVC.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [JsonIgnore]
        public ICollection<Book> Books { get; set; }
    }
}