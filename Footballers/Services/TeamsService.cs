using Footballers.Abstractions;
using Footballers.Models;

namespace Footballers.Services
{
    public class TeamsService : ITeamsService
    {
        private readonly ITeamRepository _teamRepository;

        public TeamsService(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public List<Team> GetAllTeams()
        {
            return _teamRepository.GetAllTeams();
        }

        public Team? GetTeam(int? id)
        {
            if (id != null)
            {
                return _teamRepository.GetTeam(id);
            }
            return null;
        }

        public void CreateTeam(Team team)
        {
            _teamRepository.CreateTeam(team);
        }

        public void DeleteTeam(int? Id)
        {
            if (Id != null)
            {
                _teamRepository.DeleteTeam(Id);
            }
        }
    }
}
