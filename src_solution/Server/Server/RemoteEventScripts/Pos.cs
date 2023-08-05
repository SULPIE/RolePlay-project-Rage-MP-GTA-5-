using GTANetworkAPI;

namespace Server.RemoteEventScripts
{
    public class Pos : Script
    {
        [RemoteEvent("CLIENT:SERVER::SET_POS")]
        public void Set(Player player, float X, float Y, float Z, float rotX, float rotY, float rotZ)
        {
            player.Position = new Vector3(X, Y, Z);
            player.Rotation = new Vector3(rotX, rotY, rotZ);
        }
    }
}
