using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibraryGame
{
    public class DBGamesPlayers
    {
        LoadParametros oloadParametros = new LoadParametros();        
       
        public void AddScoreGamesPlayer(ScoreGamesPlayers scoreGamesPlayers)
        {
            using (var connection = new OleDbConnection(oloadParametros.parametros.StringConnection))
            {
                connection.Open();
                string sql = "SETSCOREGAMES";
                using (OleDbCommand oCommand = connection.CreateCommand())
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.CommandText = sql;

                    oCommand.Parameters.Add("@PlayerId", SqlDbType.BigInt).Value = scoreGamesPlayers.playerId;
                    oCommand.Parameters.Add("@gameId", SqlDbType.BigInt).Value = scoreGamesPlayers.gameId;
                    oCommand.Parameters.Add("@Win", SqlDbType.BigInt).Value = scoreGamesPlayers.win;
                    oCommand.Parameters.Add("@Timestamp", SqlDbType.DateTime).Value = scoreGamesPlayers.win;


                    oCommand.ExecuteNonQuery();
                }
            }
            
        }     

       

        public List<Player> Top100Players()
        {
            List<Player> listPlayers = new List<Player>();

            using (var connection = new OleDbConnection(oloadParametros.parametros.StringConnection))
            {
                connection.Open();
                string sql = "GETTOP100PLAYERS";
                using (OleDbCommand oCommand = connection.CreateCommand())
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.CommandText = sql;

                    using (var reader = oCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            long varOut =0;

                            var player = new Player();
                            long.TryParse(reader["balance"].ToString(),out varOut);
                            player.balance = varOut;
                            
                            long.TryParse(reader["playerId"].ToString(),out varOut);
                            player.playerId = varOut;

                            DateTime oDate = new DateTime();
                            DateTime.TryParse(reader["lastUpdateDate"].ToString(), out oDate );
                            player.lastUpdateDate = oDate;

                            listPlayers.Add(player);
                        }
                    }
                }
            }

            return listPlayers;
        }
    }
}
