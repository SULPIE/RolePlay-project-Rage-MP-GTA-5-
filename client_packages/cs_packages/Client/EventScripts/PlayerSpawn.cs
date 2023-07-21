using Client.Controllers.Registration;
using RAGE;
using RAGE.Ui;
using System;

namespace Client.EventScripts
{
    internal class PlayerSpawn : Events.Script
    {
        private AuthController _authController;
        private RegController _registrationController;

        public PlayerSpawn() 
        {
            Events.OnPlayerSpawn += OnPlayerSpawn;
            Events.Add("SERVER:CLIENT::USER_EXISTING", OnServerCheckAccountExisting);
        }

        private void OnServerCheckAccountExisting(object[] args)
        {
            bool isAccountExist = (bool)args[0];
            if (isAccountExist)
            {
                _authController = new AuthController();
            }
            else
            {
                _registrationController = new RegController();
            }
        }

        private void OnPlayerSpawn(Events.CancelEventArgs cancel)
        {
            Events.CallRemote("CLIENT:SERVER::CheckAccountExisting");
        }
    }
}
