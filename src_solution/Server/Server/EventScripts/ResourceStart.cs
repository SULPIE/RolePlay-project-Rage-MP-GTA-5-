using GTANetworkAPI;
using Server.Data;
using MySql.Data.MySqlClient;
namespace Server
{
    public class Server : Script
    {
        [ServerEvent(Event.ResourceStart)]
        public void OnResourceStart()
        {
            ContextData.Init();
        }
    }
}
