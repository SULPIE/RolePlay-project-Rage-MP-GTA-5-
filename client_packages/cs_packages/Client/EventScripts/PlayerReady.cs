using RAGE;

namespace Client.EventScripts
{
    public class PlayerReady: Events.Script
    {
        public PlayerReady() 
        {
            Events.OnPlayerReady += OnPlayerReady;
        }

        public void OnPlayerReady()
        {
            Chat.Output("Добро пожаловать");
        }
    }
}
