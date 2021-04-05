using Api.CatalogoDeJogos;
using Api.CatalogoDeJogos.Data;
using Api.CatalogoDeJogos.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.CatalogoDeJogos.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly DbContext _dbContext;
        IMongoCollection<Game> _games;
        public GameRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _games = dbContext.db.GetCollection<Game>(typeof(Game).Name.ToLower());

        }

        public Task<List<Game>> GetPagination(int pag, int amount)
            =>Task.FromResult(IMongoCollectionExtensions.AsQueryable(_games).Skip((pag - 1) * amount).Take(amount).ToList());
        
        public Task<Game> GetGameById(Guid gameId)
            =>Task.FromResult(IMongoCollectionExtensions.AsQueryable(_games).FirstOrDefault(s => s.GameId == gameId.ToString()));

        public Task<List<Game>> GetGameByNameAndProduction(string name, string production)
            =>Task.FromResult(_games.FindSync(x => x.Name == name && x.Production == production).ToList()) ;
        
        public Task Create(Game game)
            =>  _games.InsertOneAsync(game);

        public Task Update(Game game)
        {
            _games.ReplaceOneAsync<Game>(a => a.GameId == game.GameId, game);;
            return Task.CompletedTask;
        }

        public Task Delete(Guid gameId)
        { 
            _games.DeleteOneAsync<Game>(game => game.GameId == gameId.ToString());
            return Task.CompletedTask;
        }
        public void Dispose() { }

    }
}
