using GTANetworkAPI;
using GTANetworkMethods;
using Server.Data;

namespace Server.EventScripts.ResourceStart
{
    public class Server : Script
    {
        [ServerEvent(Event.ResourceStart)]
        public void OnResourceStart()
        {
            ContextData.Init();
            WorldTimer.Start();
            RefreshUITimer.Start();

            Vector3 Position = new Vector3(453.45853f, -637.5926f, 28.499329f);
            Vector3 Rotation = new Vector3(0.0f, 0.0f, -103.42419f);

            NAPI.Server.SetDefaultSpawnLocation(Position);
        }
    }
}
