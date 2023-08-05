using GTANetworkAPI;
using Server.AccountInfo;

namespace Server.RemoteEventScripts.AccountPerformsData.Registration
{
    internal class ClientSkin : Script
    {
        [RemoteEvent("CLIENT:SERVER::SET_PLAYER_SKIN_INTRO")]
        public void SetClientSkin(Player player, string skin, int sex)
        {
            uint vhash = NAPI.Util.GetHashKey(skin);
            player.SetSkin(vhash);

            AccountHandlerDictionary.GetAccount(player).Skin = skin;
            AccountHandlerDictionary.GetAccount(player).Sex = sex;
        }
    }
}
