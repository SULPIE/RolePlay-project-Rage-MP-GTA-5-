using GTANetworkAPI;

namespace Server.ServerCommands
{
    public class PlayerPos : Script
    {
        [Command("setpos")]
        public void Set(Player player, float x, float y, float z)
        {
            player.Position = new Vector3(x, y, z);
        }
    }
}
