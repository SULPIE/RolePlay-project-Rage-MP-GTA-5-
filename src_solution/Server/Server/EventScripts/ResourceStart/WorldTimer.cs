using GTANetworkAPI;
using Server.AccountInfo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Server.EventScripts.ResourceStart
{
    public static class WorldTimer
    {
        private static readonly int time = 5 * 60000;
        public async static void Start()
        {
            while (true)
            {
                NAPI.Task.Run(() => NAPI.World.SetTime(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second));
                await Task.Delay(time);
            }
        }
    }
}
