using Footballers.Models;
using Footballers.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Footballers.Abstractions;
using System.Reflection.Emit;

namespace Footballers.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly ApplicationContext context;
        public PlayerRepository(ApplicationContext contextContext)
        {
            context = contextContext;
        }

        public List<Footballer> GetAllFootballers()
        {
            return context.Footballers.AsNoTracking().Include(x => x.Team).Include(y => y.Country).ToList();
        }

        public Footballer? GetFootballer(int? id) 
        { 
            return  context.Footballers.AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        public void CreateFootballer(Footballer footballer)
        {
            if (context.Footballers.FirstOrDefault(x => x.FirstName == footballer.FirstName
                    && x.SecondName == footballer.SecondName) == null)
            {
                var result = context.Add(footballer);
                context.SaveChanges();
            }
        }

        public void DeleteFootballer(int Id)
        {
            Footballer? footballer = context.Footballers.AsNoTracking().FirstOrDefault(x => x.Id == Id);
            if (footballer != null)
            {
                context.Footballers.Remove(footballer);
                context.SaveChanges();
            }
        }

        public void EditFootballer(Footballer footballer)
        {
            Footballer? Footballer = GetFootballer(footballer.Id);
            if (Footballer != null)
            {
                context.Footballers.Update(footballer);
                context.SaveChanges();
            }
        }
    }
}
