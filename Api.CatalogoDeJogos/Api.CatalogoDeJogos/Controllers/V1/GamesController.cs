using Api.CatalogoDeJogos.Exceptions;
using Api.CatalogoDeJogos.Model.InputModel;
using Api.CatalogoDeJogos.Model.ViewModel;
using Api.CatalogoDeJogos.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Api.CatalogoDeJogos.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;
        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }
        [HttpGet]
        public async Task<ActionResult<List<GameViewModel>>> GetAllGames([FromQuery, Range(1, int.MaxValue)] int pag = 1, [FromQuery, Range(1, 50)] int amount = 5)
        {
            var getAllGames = await _gameService.GetAllGames(pag, amount);
            
            return Ok(getAllGames.Count == 0 ? NoContent() : getAllGames);
        }

        [HttpGet("{gameId:guid}")]
        public async Task<ActionResult<GameViewModel>> GetGameById([FromRoute] Guid gameId, [FromServices] IGameService gameService)
        {
            var game = await gameService.GetGameById(gameId);

            return game == null ? NoContent() : game;
        }


        [HttpPost]
        public async Task<ActionResult<List<GameViewModel>>> CreteGame([FromBody] GameInputModel game)
        {
            try {
                return Ok(await _gameService.CreateGame(game));
            }
            catch (GameAlreadyRegistered ex) {
                return UnprocessableEntity("Já existe um jogo com o mesmo nome para essa produtora");
            }
        }

        [HttpPut("{gameId:guid}")]
        public async Task<ActionResult> UpdateGameById([FromRoute] Guid gameId, [FromBody]GameInputModel game)
        {
            try
            {
                await _gameService.UpdateGameById(gameId, game);
                return Ok();
            }
            catch (GameNotregistered ex)
            {
                return NotFound("O jogo não existe");
            }
        }

        [HttpPatch("{gameId:guid}/price/{price:double}")]
        public async Task<ActionResult> UpdateGamePriceById([FromRoute] Guid gameId, [FromRoute] double price) {
            try
            {
                await _gameService.UpdateGamePriceById(gameId, price);
                return Ok();
            }
            catch (GameNotregistered ex)
            {
                return NotFound("O jogo não existe");
            }
        }

        [HttpDelete("{gameId:guid}")]
        public async Task<ActionResult> DeleteGameById ([FromRoute]Guid gameId)
        {
            try
            {
                await _gameService.DeleteGameById(gameId);
                return Ok();
            }
            catch (GameNotregistered ex)
            {
                return NotFound("O jogo não existe");
            }
        }
    }
}
