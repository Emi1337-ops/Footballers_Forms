using System.ComponentModel.DataAnnotations;
using Footballers.Attributes;

namespace Footballers.Models
{
    public class Footballer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указано Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Не указана Фамилия")]
        public string SecondName { get; set; }
        [Required]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Не указана Дата Рождения")]
        [DateValidation]
        public DateOnly BirthDay { get; set; }

        [Required]
        public int TeamId { get; set; }
        public Team? Team { get; set; }

        [Required]
        public int CountryId { get; set; }
        public Country? Country { get; set; }
    }
}
