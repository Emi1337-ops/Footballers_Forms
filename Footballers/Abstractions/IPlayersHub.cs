using Footballers.Models;

namespace Footballers.Abstractions
{
    public interface IPlayersHub
    {
        Task AddFootballer(Footballer footballer);
        Task DeleteFootballer(int Id);
        Task EditFootballer(Footballer footballer);
        Task DeleteTeam(int Id);
        Task AddTeam(Team team);
    }
}