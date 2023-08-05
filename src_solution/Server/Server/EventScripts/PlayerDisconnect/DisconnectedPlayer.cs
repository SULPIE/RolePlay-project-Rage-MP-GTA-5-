using GTANetworkAPI;
using Server.AccountInfo;

namespace Server.EventScripts.PlayerDisconnect
{
    internal class DisconnectedPlayer : Script
    {
        [ServerEvent(Event.PlayerDisconnected)]
        public void OnPlayerDisconnect(Player player, DisconnectionType type, string reason)
        {
            Update.Data(player);

            AccountHandlerDictionary.RemoveAccountFromDictionary(player);
        }
    }
}
