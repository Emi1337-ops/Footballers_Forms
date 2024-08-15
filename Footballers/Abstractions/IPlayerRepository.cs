using Footballers.Models;

namespace Footballers.Abstractions
{
    public interface IPlayerRepository
    {
        void CreateFootballer(Footballer footballer);
        void DeleteFootballer(int Id);
        void EditFootballer(Footballer footballer);
        List<Footballer> GetAllFootballers();
        Footballer? GetFootballer(int? Id);
    }
}