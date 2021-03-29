using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.CatalogoDeJogos.Entities;

namespace Api.CatalogoDeJogos.Repositories
{
    public interface IGameRepository : IDisposable
    {
        Task<List<Game>> GetPagination(int pag, int amount);
        Task<Game> GetGameById(Guid gameId);

        Task<List<Game>> GetGameByNameAndProduction(string name, string production);
        Task Create(Game game);

        Task Update(Game game);

        Task Delete(Guid gameId);
    }
}
