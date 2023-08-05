using Client.Controllers.Registration;
using RAGE;

namespace Client.EventScripts
{
    public class PlayerReady: Events.Script
    {
        private AuthController _authController;
        private RegController _registrationController;
        public PlayerReady() 
        {
            Events.OnPlayerReady += OnPlayerReady;
            Events.Add("SERVER:CLIENT::USER_EXISTING", OnServerCheckAccountExisting);
        }

        private void OnServerCheckAccountExisting(object[] args)
        {
            Chat.Output("Добро пожаловать");

            bool isAccountExist = (bool)args[0];

            if (isAccountExist)
            {
                _authController = new AuthController(true);
            }
            else
            {
                _registrationController = new RegController();
            }
        }

        private void OnPlayerReady()
        {
            Events.CallRemote("CLIENT:SERVER::CheckAccountExisting");
        }
    }
}
