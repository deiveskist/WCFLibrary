using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace WcfServiceLibraryGame
{
    [ServiceContract]

    public interface IServiceGames
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/AddScoreGamePlayersPOST")]
        String AddScoreGamePlayersPOST(ScoreGamesPlayers scoreGamesPlayers);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "AddScoreGamePlayers/{playerId};{gameId};{win};{timestamp}")]
        String AddScoreGamePlayersGET(String playerId, String gameId, String win, String timestamp);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/GetTop100Players")]
        List<Player> GetTop100Players();
    }
}
