using System.ComponentModel.DataAnnotations;

namespace LibraryMVC.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Blurb { get; set; }
    }
}