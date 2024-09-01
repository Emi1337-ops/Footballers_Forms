using Footballers.Abstractions;
using Footballers.Models;
using Microsoft.AspNetCore.SignalR;

namespace Footballers.Hubs
{
    public class PlayersHub : Hub, IPlayersHub
    {
        private readonly IHubContext<PlayersHub> context;

        public PlayersHub(IHubContext<PlayersHub> Context)
        {
            context = Context;
        }
        public async Task AddFootballer(Footballer footballer)
        {
            await context.Clients.All.SendAsync("ReceiveNewFootballer", 
                footballer.Id, 
                footballer.FirstName,
                footballer.SecondName, 
                footballer.Team.Name, 
                footballer.Team.Id,
                footballer.Country.Name,
                footballer.Gender, 
                footballer.BirthDay.ToString());
        }

        public async Task DeleteFootballer(int Id)
        {
            await context.Clients.All.SendAsync("ReceiveDeleteFootballer", Id);
        }

        public async Task EditFootballer(Footballer footballer)
        {
            await context.Clients.All.SendAsync("ReceiveEditFootballer", 
                footballer.Id, 
                footballer.FirstName,
                footballer.SecondName,
                footballer.Team.Name, 
                footballer.Country.Name,
                footballer.Gender, 
                footballer.BirthDay.ToString());
        }

        public async Task DeleteTeam(int Id)
        { 
            await context.Clients.All.SendAsync("ReceiveDeleteTeam", Id);
        }

        public async Task AddTeam(Team team)
        {
            await context.Clients.All.SendAsync("ReceiveNewTeam", team.Id, team.Name);
        }
    }
}
