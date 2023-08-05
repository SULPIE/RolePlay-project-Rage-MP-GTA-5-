using GTANetworkAPI;
using System.Collections.Generic;

namespace Server.ChatSystem
{
    class Utils
    {
        public static void ProxDetector(Player player, string message, double radius)
        {
            List<Player> _nearby_players = NAPI.Player.GetPlayersInRadiusOfPlayer(radius, player);

            foreach(Player _player in _nearby_players)
            {
                if(_player.Dimension == player.Dimension)
                {
                    _player.SendChatMessage($"{message}");
                }
            }
        }
    }
}
