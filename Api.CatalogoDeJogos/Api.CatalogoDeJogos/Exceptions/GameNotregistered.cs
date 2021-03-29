using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.CatalogoDeJogos.Exceptions
{
    public class GameNotregistered : Exception
    {
        public GameNotregistered() : base("Este jogo não está cadastrado") { }
    }
}
