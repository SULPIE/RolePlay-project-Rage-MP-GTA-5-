using GTANetworkAPI;
using Server.AccountInfo;

namespace Server.ServerCommands
{
    public class DataTest : Script
    {
        [Command("tests")]
        public void Read(Player player)
        {
            player.SendChatMessage("" + AccountHandlerDictionary.GetAccount(player).Name);
            player.SendChatMessage("" + player.Name);
            NAPI.Util.ConsoleOutput("" + AccountHandlerDictionary.DictionarySize().ToString());
        }
    }
}
