using GTANetworkAPI;
using Server.AccountInfo;

namespace Server.RemoteEventScripts.DisconnectedPlayerRemote
{
    public class DisconnectedPlayerRemote
    {
        [RemoteEvent("CLIENT:SERVER::ON_PLAYER_DISCONNECT")]
        public void OnPlayerDisconnect(Player player)
        {
        }
    }
}
