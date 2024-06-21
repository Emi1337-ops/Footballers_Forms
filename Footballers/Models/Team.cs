using System.ComponentModel.DataAnnotations;

namespace Footballers.Models
{
    public class Team
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public List<Footballer>? Footballers { get; set; }
    }
}
