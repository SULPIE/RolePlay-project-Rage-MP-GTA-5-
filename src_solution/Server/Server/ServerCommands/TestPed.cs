
using GTANetworkAPI;
using Server.AccountInfo;
using System.Collections.Generic;

namespace Server.ServerCommands
{
    internal class TestPed : Script
    {
        [Command("setblend")]
        public void SetSkin(Player playerid, string skinname)
        {
            uint vhash = NAPI.Util.GetHashKey(skinname);
            playerid.SetSkin(vhash);
        }
    }
}
