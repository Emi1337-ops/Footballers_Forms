using Footballers.Models;

namespace Footballers.ViewModels
{
    public class FormViewModel
    {
        public Team Team { get; set; }
        public Footballer Footballer { get; set; }
        public IEnumerable<Team> Teams { get; set; }
        public IEnumerable<Country> Countries { get; set; }
    }
}
