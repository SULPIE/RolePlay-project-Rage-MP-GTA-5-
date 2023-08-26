using GTANetworkAPI;

namespace Server.ServerCommands
{
    public class PlayerPos : Script
    {
        [Command("setpos")]
        public void Set(Player player, float x, float y, float z, string IPL)
        {
            NAPI.World.RequestIpl(IPL);
            player.Position = new Vector3(x, y, z);
            NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::DOWNLOAD_PROP_INTERIOR", 1);
        }
    }
}
