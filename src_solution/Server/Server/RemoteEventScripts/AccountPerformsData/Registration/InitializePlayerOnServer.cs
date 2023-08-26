using GTANetworkAPI;
using Server.AccountInfo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Server.RemoteEventScripts.AccountPerformsData.Registration
{
    public static class InitializePlayerOnServer
    {
        [RemoteEvent("CLIENT:SERVER::INITIALIZE_ON_SERVER")]
        public static void Start(Player player)
        {
            Account account = AccountHandlerDictionary.GetAccount(player);

            if (account != null) 
            {
                uint vhash = NAPI.Util.GetHashKey(account.Skin);
                NAPI.Task.Run(() => {
                    player.SetSkin(vhash);
                    player.Dimension = 0;

                    Utils.IPLs.Load();
                }, 500);
            }
        }
    }
}
