using System;

namespace Api.CatalogoDeJogos.Model.ViewModel
{
    public class GameViewModel
    {
        public Guid GameId { get; set; }
        public string Name { get; set; }
        public string Production { get; set; }
        public double Price { get; set; }

    }
}
