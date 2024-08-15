using Footballers.Models;

namespace Footballers.Abstractions
{
    public interface ITeamsService
    {
        void CreateTeam(Team team);
        void DeleteTeam(int? Id);
        List<Team> GetAllTeams();
        Team? GetTeam(int? id);
    }
}