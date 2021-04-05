using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.CatalogoDeJogos.Exceptions
{
    public class GameAlreadyRegistered : Exception
    {
        public GameAlreadyRegistered() : base ("O jogo já está cadastrado")
        {

        }
    }
}
