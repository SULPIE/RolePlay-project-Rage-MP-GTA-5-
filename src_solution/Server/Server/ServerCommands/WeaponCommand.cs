using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.ServerCommands
{
    internal class WeaponCommand : Script
    {
        [Command("gw")]
        public void Give(Player player, WeaponHash whash)
        {
            player.GiveWeapon(whash, 9999);
        }
    }
}
