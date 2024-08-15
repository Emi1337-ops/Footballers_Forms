using Footballers.Abstractions;
using Footballers.Models;
using Footballers.ViewModels;
using Microsoft.AspNetCore.SignalR;

namespace Footballers.Repositories
{
    public class FormViewRepository : IFormViewRepository
    {
        private readonly ApplicationContext context;

        public FormViewRepository(ApplicationContext dbContext)
        {
            context = dbContext;
        }

        public FormViewModel Get()
        {
            var viewmodel = new FormViewModel()
            {
                Teams = context.Teams.ToList(),
                Countries = context.Countries.ToList(),
                Genders = new string[] { "Мужской", "Женский" }
            };
            return viewmodel;
        }

        public FormViewModel Get(int id)
        {
            Footballer? footballer = context.Footballers.Where(x => x.Id == id).FirstOrDefault();
            var viewmodel = new FormViewModel()
            {
                Footballer = footballer,
                Teams = context.Teams.ToList(),
                Countries = context.Countries.ToList(),
                Genders = new string[] { "Мужской", "Женский" }
            };
            return viewmodel;
        }
    }
}
