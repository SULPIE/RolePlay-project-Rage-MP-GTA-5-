using RAGE;
using RAGE.Elements;
using System;

namespace Client.EventScripts
{
    internal class PlayerDisconnect : Events.Script
    {
        public PlayerDisconnect() 
        {
            Events.OnPlayerQuit += OnPlayerDisconnect;
        }

        private void OnPlayerDisconnect(Player player)
        {
            Events.CallRemote("CLIENT:SERVER::ON_PLAYER_DISCONNECT");
        }
    }
}
