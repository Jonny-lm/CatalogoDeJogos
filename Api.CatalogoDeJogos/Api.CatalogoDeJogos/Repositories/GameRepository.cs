using Api.CatalogoDeJogos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.CatalogoDeJogos.Repositories
{
    public class GameRepository : IGameRepository
    {

        private static Dictionary<Guid, Game> games = new Dictionary<Guid, Game>()
        {
            {Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), new Game{ GameId = Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), Name = "Fifa 21", Production = "EA", Price = 200} },
            {Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), new Game{ GameId = Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), Name = "Fifa 20", Production = "EA", Price = 190} },
            {Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), new Game{ GameId = Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), Name = "Fifa 19", Production = "EA", Price = 180} },
            {Guid.Parse("da033439-f352-4539-879f-515759312d53"), new Game{ GameId = Guid.Parse("da033439-f352-4539-879f-515759312d53"), Name = "Fifa 18", Production = "EA", Price = 170} },
            {Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), new Game{ GameId = Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), Name = "Street Fighter V", Production = "Capcom", Price = 80} },
            {Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), new Game{ GameId = Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), Name = "Grand Theft Auto V", Production = "Rockstar", Price = 190} }
        };

        public Task<List<Game>> GetPagination(int pag, int amount)
            => Task.FromResult(games.Values.Skip((pag - 1) * amount).Take(amount).ToList());

        public Task<Game> GetGameById(Guid gameId)
            => !games.ContainsKey(gameId) ? Task.FromResult<Game>(null) : Task.FromResult(games[gameId]);

        public Task<List<Game>> GetGameByNameAndProduction(string name, string production)
            => Task.FromResult(games.Values.Where(jogo => jogo.Name.Equals(name) && jogo.Production.Equals(production)).ToList());

        public Task Create(Game game)
        {
            games.Add(game.GameId, game);
            return Task.CompletedTask;
        }

        public Task Update(Game game)
        {
            games[game.GameId] = game;
            return Task.CompletedTask;
        }

        public Task Delete(Guid gameId)
        {
            games.Remove(gameId);
            return Task.CompletedTask;
        }

        public void Dispose() { }
    }
}
