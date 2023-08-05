using GTANetworkAPI;

namespace Server.ChatSystem
{
    public class Core : Script
    {
        [ServerEvent(Event.ChatMessage)]
        public void OnPlayerSendMessage(Player player, string message)
        {
            Utils.ProxDetector(player, $"{Server.Utils.Colors.COLOR_GRAY}{player.Name} ~w~говорит: {message}", 20);
        }
    }
}
