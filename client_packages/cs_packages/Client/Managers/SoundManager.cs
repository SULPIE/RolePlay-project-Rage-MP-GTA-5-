using RAGE;
using System;

namespace Client.Managers
{
    public class SoundManager
    {
        public SoundManager()
        {
            Events.Add("SERVER:CLIENT::PLAY_AUDIO_CONFIRM_BEEP", playSoundConfirmBeep);
        }

        private void playSoundConfirmBeep(object[] args)
        {
            RAGE.Game.Audio.PlaySoundFrontend(-1, "BUTTON", "MP_PROPERTIES_ELEVATOR_DOORS", true);
        }
    }
}
