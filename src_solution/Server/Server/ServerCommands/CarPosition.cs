using GTANetworkAPI;

namespace Server.ServerCommands
{
    public class CarPosition: Script
    {
        [Command("getcarpos")]
        public void Get(Player player) 
        {
            Vector3 pos = player.Vehicle.Position;
            Vector3 rot = player.Vehicle.Rotation;
            NAPI.Util.ConsoleOutput($"{pos.X}, {pos.Y}, {pos.Z}, {rot.X}, {rot.Y}, {rot.Z}");
        }
    }
}
