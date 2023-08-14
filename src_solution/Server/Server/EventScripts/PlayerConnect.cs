using GTANetworkAPI;

namespace Server.EventScripts
{
    internal class PlayerConnect
    {
        [ServerEvent(Event.PlayerConnected)]
        public void PlayerConnected(Player player)
        {
            player.Dimension = player.Id;
        }
    }
}
