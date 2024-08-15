using Footballers.Abstractions;
using Footballers.Models;
using Microsoft.EntityFrameworkCore;

namespace Footballers.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly ApplicationContext context;
        public TeamRepository(ApplicationContext contextContext)
        {
            context = contextContext;
        }

        public List<Team> GetAllTeams()
        {
            return context.Teams.AsNoTracking().ToList();
        }

        public Team? GetTeam(int? id) 
        {
            return context.Teams.AsNoTracking().FirstOrDefault(t => t.Id == id);
        }

        public void CreateTeam(Team team)
        {
            if (context.Teams.AsNoTracking().FirstOrDefault(x => x.Name == team.Name) == null)
            {
                context.Add(team);
                context.SaveChanges();
            }
        }

        public void DeleteTeam(int? id)
        {
            Team? team = GetTeam(id);
            if (team != null)
            {
                context.Teams.Remove(team);
                context.SaveChanges();
            }
        }
    }
}
