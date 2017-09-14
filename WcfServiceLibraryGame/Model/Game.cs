using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibraryGame
{
    public class Game
    {
        public Game(long id)
        {
            gameId = id;
        }

        public long gameId;
        public String nameGame;
    }
}
