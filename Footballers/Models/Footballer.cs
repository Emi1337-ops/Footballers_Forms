using System.ComponentModel.DataAnnotations;

namespace Footballers.Models
{
    public class Footballer
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string SecondName { get; set; }
        [Required]
        public DateOnly BirthDay { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public int TeamId { get; set; }
        public Team? Team { get; set; }
        [Required]
        public int CountryId { get; set; }
        public Country? Country { get; set; }
    }
}
