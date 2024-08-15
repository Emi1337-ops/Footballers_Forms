using System.ComponentModel.DataAnnotations.Schema;

namespace Footballers.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Footballer>? Footballers { get; set; }
    }
}
