using System.ComponentModel.DataAnnotations;

namespace LibraryMVC.Dtos.Authors
{
    public class AuthorAddEditDto
    {
        public int Id { get; set; }
        
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}