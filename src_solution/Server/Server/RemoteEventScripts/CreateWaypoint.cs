using GTANetworkAPI;

namespace Server.RemoteEventScripts
{
    public class CreateWaypoint : Script
    {
        [RemoteEvent("CLIENT:SERVER::CREATE_WAYPOINT")]
        public void OnClientCreateWaypoint(Player player, float X, float Y, float Z)
        {
            player.Position = new Vector3(X, Y, Z);
        }
    }
}
