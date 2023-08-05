using GTANetworkAPI;
using Server.AccountInfo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.ServerCommands
{
    internal class GiveMoney : Script
    {
        [Command("givemoney")]
        public void GiveMoneyToPlayer(Player player)
        {
            AccountHandlerDictionary.GetAccount(player).Money += 1000;
        }
    }
}
