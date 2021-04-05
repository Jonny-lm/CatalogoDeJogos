using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;

namespace Api.CatalogoDeJogos.Entities
{
    public class Game
    {
        public string GameId { get; set; }
        public string Name { get; set; }
        public string Production { get; set; }
        public double Price { get; set; }
    }
}
