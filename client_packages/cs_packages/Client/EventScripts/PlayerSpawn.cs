using Client.Controllers.Registration;
using RAGE;
using RAGE.Elements;
using RAGE.Ui;
using RAGE.Util;
using System;

namespace Client.EventScripts
{
    internal class PlayerSpawn : Events.Script
    {
        public PlayerSpawn() 
        {
            Events.OnPlayerSpawn += OnPlayerSpawn;
        }

        private void OnPlayerSpawn(Events.CancelEventArgs cancel)
        {
            //RAGE.Game.Streaming.RequestAnimDict()
        }
    }
}
