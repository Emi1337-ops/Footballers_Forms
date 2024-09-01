using Footballers.Abstractions;
using Footballers.Hubs;
using Footballers.Models;
using Footballers.Repositories;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Footballers.Services
{
    public class PlayersService : IPlayersService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IPlayersHub _playersHub;

        public PlayersService(IPlayerRepository playerRepository, IPlayersHub playersHub)
        {
            _playerRepository = playerRepository;
            _playersHub = playersHub;
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
            var player = _playerRepository.GetFootballer(footballer.Id);
            if (player != null)
            { _playersHub.AddFootballer(player); }
            
            
        }

        public void DeleteFootballer(int Id)
        {
            _playerRepository.DeleteFootballer(Id);
            _playersHub.DeleteFootballer(Id);
        }

        public void EditFootballer(Footballer footballer)
        {
            _playerRepository.EditFootballer(footballer);
            _playersHub.EditFootballer(_playerRepository.GetFootballer(footballer.Id));
        }
    }
}
