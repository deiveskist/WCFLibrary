using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibraryGame
{
   // [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ServiceGames : IServiceGames
    {
        static DBGamesPlayers dbGamesPlayers = new DBGamesPlayers();

        public String AddScoreGamePlayersPOST(ScoreGamesPlayers scoreGamesPlayers)
        {
            try
            {
               
                dbGamesPlayers.AddScoreGamesPlayer(scoreGamesPlayers);

                return "OK";
            }
            catch (Exception ex)
            {
                return "Request Error : " + ex.Message;
            }
        }

        public String AddScoreGamePlayersGET(String playerId, String gameId, String win, String timestamp)
        {
            try
            {
                ScoreGamesPlayers scoreGamesPlayers = new ScoreGamesPlayers();
                var outVar = 0;
                int.TryParse(playerId, out outVar);
                scoreGamesPlayers.playerId = outVar;


                int.TryParse(gameId, out outVar);
                scoreGamesPlayers.gameId = outVar;


                int.TryParse(win, out outVar);
                scoreGamesPlayers.win = outVar;

                DateTime dttimestamp = new DateTime();
                DateTime.TryParse(timestamp, out dttimestamp);

                scoreGamesPlayers.timestamp = dttimestamp;
                               

                dbGamesPlayers.AddScoreGamesPlayer(scoreGamesPlayers);

                return "OK";
            }
            catch (Exception ex)
            {
                return "Request Error : " + ex.Message;
            }
        }


        public List<Player> GetTop100Players()
        {
            return dbGamesPlayers.Top100Players();
        }
    }
}
