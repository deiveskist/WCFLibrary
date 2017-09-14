using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibraryGame
{
    [DataContract]
    public class ScoreGamesPlayers
    {
        [DataMember]
        public long	playerId { get; set; } //ID do jogador

        [DataMember]
        public long gameId { get; set; } //ID do jogo 

        [DataMember]
        public long win { get; set; } //o número de pontos ganhos (positivos ou negativos)

        [DataMember]
        public DateTime timestamp; // data de quando o jogo foi realizado(UTC)

     }
}
