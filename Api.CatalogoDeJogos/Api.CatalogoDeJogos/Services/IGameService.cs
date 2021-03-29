using Api.CatalogoDeJogos.Model.InputModel;
using Api.CatalogoDeJogos.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.CatalogoDeJogos.Services
{
    public interface IGameService : IDisposable
    {
        Task<List<GameViewModel>> GetAllGames(int pag, int amount);
        Task<GameViewModel> GetGameById(Guid gameId);

        Task<GameViewModel> CreateGame(GameInputModel game);

        Task UpdateGameById(Guid gameId, GameInputModel game);

        Task UpdateGamePriceById(Guid gameId, double price);
        Task DeleteGameById(Guid gameId);
    }
}
