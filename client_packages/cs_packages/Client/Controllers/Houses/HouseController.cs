using Client.Managers;
using RAGE;
using RAGE.Ui;
using System;
using System.Collections.Generic;

namespace Client.Controllers.Houses
{
    public class HouseController : Events.Script
    {
        private HtmlWindow _window = new HtmlWindow("package://CEF/houses/index.html");

        public HouseController() 
        {
            Events.Add("SERVER:CLIENT::ON_PLAYER_PICKUP_HOUSE_PICKUP", OnPlayerPickUpHousePickup);
            Events.Add("SERVER:CLIENT::ON_PLAYER_EXIT_HOUSE_PICKUP", OnPlayerExitHousePickup);
            Events.Add("SERVER:CLIENT::ON_PLAYER_BUY_HOUSE", OnPlayerTrynaBuyHouse);
            Events.Add("CEF:CLIENT::ON_CONFIRM_BUTTON_CLICK", OnPlayerClickConfirm);
            Events.Add("CEF:CLIENT::ON_CANCEL_BUTTON_CLICK", OnPlayerClickCancel);

            _window.Active = false;
        }

        private void OnPlayerPickUpHousePickup(object[] args)
        {
            _window.Active = true;

            _window.Call("CLIENT:CEF::DRAW_UI_HOUSE", (string)args[0], (string)args[1], (string)args[2], (string)args[3], (string)args[4]);

            Events.Tick += PressKeyListener;
        }

        private void OnPlayerExitHousePickup(object[] args)
        {
            _window.Active = false;
            Cursor.ShowCursor(false, false);
            Events.Tick -= PressKeyListener;
        }

        private void PressKeyListener(List<Events.TickNametagData> nametags)
        {
            KeyManager.KeyBind(KeyManager.KeyEnter, () =>
            {
                Events.CallRemote("CLIENT:SERVER::ON_PLAYER_PRESSED_TO_ENTER_HOUSE");
            });
        }

        private void OnPlayerTrynaBuyHouse(object[] args)
        {
            _window.Call("CLIENT:CEF::SHOW_CONFIRMATION", (string)args[0], (string)args[1]);
            Cursor.ShowCursor(true, true);
        }

        private void OnPlayerClickConfirm(object[] args)
        {
            Cursor.ShowCursor(false, false);
            Events.CallRemote("CLIENT:SERVER::ON_PLAYER_CONFIRM_ON_BUY");
            _window.Call("CLIENT:CEF::HIDE_CONFIRMATION");
        }

        private void OnPlayerClickCancel(object[] args)
        {
            Cursor.ShowCursor(false, false);
            _window.Call("CLIENT:CEF::HIDE_CONFIRMATION");
        }
    }
}
