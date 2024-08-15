using Footballers.Models;

namespace Footballers.Abstractions
{
    public interface IPlayersService
    {
        void CreateFootballer(Footballer footballer);
        void DeleteFootballer(int Id);
        void EditFootballer(Footballer footballer);
        List<Footballer> GetAllFootballer();
        Footballer? GetFootballer(int? id);
    }
}