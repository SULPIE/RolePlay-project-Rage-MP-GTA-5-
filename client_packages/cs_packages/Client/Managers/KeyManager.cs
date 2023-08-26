using System;
using RAGE;

namespace Client.Managers
{
    public class KeyManager : RAGE.Events.Script
    {
        private const int ResetTime = 250;    // Time to reset the key
        private static bool _keyStatus = true; // The state of the key

        public const int KeyMouse = 0xC0;    //    Key [~]
        public const int KeyLctrl = 0xA2;    //    Key [Left CTRL]
        public const int KeyN = 0x4E;
        public const int KeyM = 0x4D;
        public const int KeyUp = 0x26;
        public const int KeyDown = 0x28;
        public const int KeyEnter = 0x0D;
        public const int KeyK = 0x4B;
        public const int KeyL = 0x4C;

        public static void KeyBind(int key, Action action)
        {
            if (!Input.IsDown(key) || !_keyStatus) return;
            if (!_keyStatus) return;
            action.Invoke();
            _keyStatus = false;
            System.Threading.Tasks.Task.Delay(ResetTime).ContinueWith((task) => { _keyStatus = true; });
        }
    }
}
