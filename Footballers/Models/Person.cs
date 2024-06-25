using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Footballers.Models
{
    public class Person
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}
