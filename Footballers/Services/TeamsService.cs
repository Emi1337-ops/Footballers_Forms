
using Footballers.Abstractions;
using Footballers.Hubs;
using Footballers.Models;

namespace Footballers.Services
{
    public class TeamsService : ITeamsService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IPlayersHub hub;

        public TeamsService(ITeamRepository teamRepository, IPlayersHub Hub)
        {
            _teamRepository = teamRepository;
            hub = Hub;
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
            var Team = _teamRepository.GetTeam(team.Id);
            if (Team != null)
            { hub.AddTeam(team); }
                
        }

        public void DeleteTeam(int? Id)
        {
            if (Id != null)
            {
                _teamRepository.DeleteTeam(Id);
                hub.DeleteTeam(Id.Value);
            }
        }
    }
}
