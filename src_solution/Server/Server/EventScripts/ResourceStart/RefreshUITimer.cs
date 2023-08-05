using GTANetworkAPI;
using Server.AccountInfo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Server.EventScripts.ResourceStart
{
    static public class RefreshUITimer
    {
        private static readonly int time = 1000;
        public async static void Start()
        {
            while (true)
            {
                foreach (var player in NAPI.Pools.GetAllPlayers())
                {
                    Account account = AccountHandlerDictionary.GetAccount(player);
                    if (account != null)
                    {
                        NAPI.Task.Run(() => {
                            NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::ON_DRAW_UI", account.Money, player.Position.X, player.Position.Y, player.Position.Z);
                        }
                        );
                    }
                }
                await Task.Delay(time);
            }
        }
    }
}
