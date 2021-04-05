using Api.CatalogoDeJogos.Entities;
using Api.CatalogoDeJogos.Exceptions;
using Api.CatalogoDeJogos.Model.InputModel;
using Api.CatalogoDeJogos.Model.ViewModel;
using Api.CatalogoDeJogos.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.CatalogoDeJogos.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<List<GameViewModel>> GetAllGames(int pag, int amount)
            => (await _gameRepository.GetPagination(pag, amount)).Select(x => new GameViewModel
            {
                GameId = Guid.Parse(x.GameId),
                Name = x.Name,
                Price = x.Price,
                Production = x.Production
            }).ToList();


        public async Task<GameViewModel> GetGameById(Guid gameId)
        {
            var game = await _gameRepository.GetGameById(gameId);

            return game == null ? null : new GameViewModel
            {
                GameId = Guid.Parse(game.GameId),
                Name = game.Name,
                Price = game.Price,
                Production = game.Production
            };

        }

        public async Task<GameViewModel> CreateGame(GameInputModel game)
        {
            var entGame = await _gameRepository.GetGameByNameAndProduction(game.Name, game.Production);
            if (entGame.Count() > 0) throw new GameAlreadyRegistered();
            var createGame =  new Game
            {
                GameId = Guid.NewGuid().ToString(),
                Name = game.Name,
                Price = game.Price,
                Production = game.Production
            };

            await _gameRepository.Create(createGame);

            return new GameViewModel
            {
                GameId = Guid.Parse(createGame.GameId),
                Name = createGame.Name,
                Production = createGame.Production,
                Price = createGame.Price
            };

        }

        public async Task UpdateGameById(Guid gameId, GameInputModel game)
        {
            var oldGame = await _gameRepository.GetGameById(gameId);
            if (oldGame == null) { throw new GameNotregistered(); };

            oldGame.Price = game.Price;
            oldGame.Name = game.Name;
            oldGame.Production = game.Production;

            await _gameRepository.Update(oldGame);

        }

        public async Task UpdateGamePriceById(Guid gameId, double price)
        {
            var game = await _gameRepository.GetGameById(gameId);
            if (game == null) { throw new GameNotregistered(); };

            game.Price = price;

            await _gameRepository.Update(game);

        }
        public async Task DeleteGameById(Guid gameId)
        {
            var getGame = _gameRepository.GetGameById(gameId);
            if (getGame == null) throw new GameAlreadyRegistered();
            await _gameRepository.Delete(gameId);
        }


        public void Dispose()
        {
            _gameRepository?.Dispose();
        }

    }
}
