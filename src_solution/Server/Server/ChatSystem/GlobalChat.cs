using GTANetworkAPI;

namespace Server.ChatSystem
{
    internal class GlobalChat : Script
    {
        [ServerEvent(Event.ResourceStart)]
        public void DisableGlobalChat()
        {
            NAPI.Server.SetGlobalServerChat(false);
        }
    }
}
