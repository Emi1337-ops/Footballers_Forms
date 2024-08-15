using Footballers.Models;

namespace Footballers.Abstractions
{
    public interface ITeamRepository
    {
        void CreateTeam(Team team);
        void DeleteTeam(int? id);
        List<Team> GetAllTeams();
        Team? GetTeam(int? id);
    }
}