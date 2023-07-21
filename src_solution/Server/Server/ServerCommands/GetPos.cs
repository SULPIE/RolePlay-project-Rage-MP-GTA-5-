using GTANetworkAPI;

namespace Server.ServerCommands
{
    internal class GetPos : Script
    {
       [Command("getpos")]
       public void CmdGetPos(Player player)
       {
            Vector3 pos = player.Position;
            Vector3 rot = player.Rotation;
            NAPI.Util.ConsoleOutput($"{pos.X}, {pos.Y}, {pos.Z}, {rot.X}, {rot.Y}, {rot.Z}");
       }
    }
}
