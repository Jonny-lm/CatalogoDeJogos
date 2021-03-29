using System;

namespace Api.CatalogoDeJogos.Entities
{
    public class Game
    {
        public Guid GameId { get; set; }
        public string Name { get; set; }
        public string Production { get; set; }
        public double Price { get; set; }
    }
}
