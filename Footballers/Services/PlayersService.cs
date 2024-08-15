using Footballers.Abstractions;
using Footballers.Models;
using Footballers.Repositories;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Footballers.Services
{
    public class PlayersService : IPlayersService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayersService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public List<Footballer> GetAllFootballer()
        {
            return _playerRepository.GetAllFootballers();
        }

        public Footballer? GetFootballer(int? id)
        {
            if (id != null)
            {
                return _playerRepository.GetFootballer(id);
            }
            return null;
        }

        public void CreateFootballer(Footballer footballer)
        {
            _playerRepository.CreateFootballer(footballer);
        }

        public void DeleteFootballer(int Id)
        {
            _playerRepository.DeleteFootballer(Id);
        }

        public void EditFootballer(Footballer footballer)
        {
            _playerRepository.EditFootballer(footballer);
        }
    }
}
