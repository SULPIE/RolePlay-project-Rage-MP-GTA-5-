using Client.Managers;
using RAGE;
using RAGE.Ui;
using System;
using System.Collections.Generic;

namespace Client.Controllers.CarShowRoom
{
    public class ShowRoomController
    {
        private HtmlWindow _window;
        private int _amount_of_cars = 0;
        private int _current_car_counter = 0;
        private int _current_car_color = 0;

        private bool _is_user_pressed_confirm_button = false;

        private const int _max_color = 158;
        public ShowRoomController() 
        {
            Events.Add("SERVER:CLIENT::DRAW_SHOWROOM_UI", InitShowRoom);
            Events.Add("SERVER:CLIENT::REFRESH_UI", RefreshUI);
            Events.Add("SERVER:CLIENT::PLAYER_BOUGHT_CAR", RemoveShowRoom);
        }

        private void RemoveShowRoom(object[] args)
        {
            _window.Destroy();
            Cursor.ShowCursor(false, false);
            RAGE.Events.Tick -= PressKeyListener;
        }

        private void RefreshUI(object[] args)
        {
            _window.Call("CLIENT:CEF::DRAW_UI_AUTOROOM", (string)args[0], (string)args[1]);
        }

        private void InitShowRoom(object[] args)
        {
            try
            {
                _window = new HtmlWindow("package://CEF/carshowroom/index.html");
                _window.Active = true;
                _window.Call("CLIENT:CEF::DRAW_UI_AUTOROOM", (string)args[0], (string)args[1]);

                _amount_of_cars = (int)args[2];

                RAGE.Events.Tick += PressKeyListener;

                Events.Add("CEF:CLIENT::ON_CONFIRM_BUTTON_CLICK", OnButtonConfirmClick);
                Events.Add("CEF:CLIENT::ON_CANCEL_BUTTON_CLICK", OnButtonCancelClick);
            }
            catch(Exception e) 
            {
                System.Console.WriteLine(e.ToString());    
            }
        }

        private void OnButtonCancelClick(object[] args)
        {
            _window.Call("CLIENT:CEF::HIDE_CONFIRMATION");
            Cursor.ShowCursor(false, false);

            _is_user_pressed_confirm_button = false;
        }
         
        private void OnButtonConfirmClick(object[] args)
        {
            if(_is_user_pressed_confirm_button == true) { return;  }

            Events.CallRemote("CLIENT:SERVER::CLICK_BUTTON_CONFIRM", _current_car_counter, _current_car_color, _max_color);

            Events.Add("SERVER:CLIENT::NOT_ENOUGH_MONEY_HANDLER", OnButtonCancelClick);

            _is_user_pressed_confirm_button = true;
        }

        private void PressKeyListener(List<Events.TickNametagData> nametags)
        {
            KeyManager.KeyBind(KeyManager.KeyN, () =>
            {
                if(_current_car_counter == 0) { return; }
                _current_car_counter--;

                Events.CallRemote("CLIENT:SERVER::ON_BUTTON_CHANGE_CAR_CLICK", _current_car_counter);
            });

            KeyManager.KeyBind(KeyManager.KeyM, () =>
            {
                if(_current_car_counter == _amount_of_cars - 1) { return; }
                _current_car_counter++;
                _current_car_color = 0;

                Events.CallRemote("CLIENT:SERVER::ON_BUTTON_CHANGE_CAR_CLICK", _current_car_counter);
            });

            KeyManager.KeyBind(KeyManager.KeyM, () =>
            {
                if (_current_car_counter == _amount_of_cars - 1) { return; }
                _current_car_counter++;
                _current_car_color = 0;

                Events.CallRemote("CLIENT:SERVER::ON_BUTTON_CHANGE_CAR_CLICK", _current_car_counter);
            });

            KeyManager.KeyBind(KeyManager.KeyUp, () =>
            {
                if (_current_car_color == _max_color) { return; }
                _current_car_color++;

                Events.CallRemote("CLIENT:SERVER::ON_CHANGE_COLOR", _current_car_color);
            });

            KeyManager.KeyBind(KeyManager.KeyDown, () =>
            {
                if (_current_car_color == 0) { return; }
                _current_car_color--;

                Events.CallRemote("CLIENT:SERVER::ON_CHANGE_COLOR", _current_car_color);
            });

            KeyManager.KeyBind(KeyManager.KeyEnter, () =>
            {
                _window.Call("CLIENT:CEF::SHOW_CONFIRMATION");

                Cursor.ShowCursor(true, true);
            });
        }
    }
}
