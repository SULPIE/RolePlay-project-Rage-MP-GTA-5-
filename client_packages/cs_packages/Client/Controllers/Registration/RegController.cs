using RAGE.Elements;
using RAGE.Game;
using RAGE.Ui;
using RAGE;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Controllers.Registration
{
    internal class RegController
    {
        private AuthController _authController;

        private static HtmlWindow authWindow;
        private static int camera = 0;
        

        private static string _user_email;
        private static string _user_password;
        
        public RegController() 
        {
            Events.Add("CEF:CLIENT:ON_BUTTON_REG_CLICK", OnButtonRegClick);
            Events.Add("SERVER:CLIENT::EMAIL_EXISTS", OnEmailExists);
            Events.Add("SERVER:CLIENT::EMAIL_DOESNT_EXIST", OnEmailDoesntExist);
            Events.Add("SERVER:CLIENT::ON_ACCOUNT_CREATE", OnCreateAccount);

            DrawRegUi();
        }

        private void OnCreateAccount(object[] args)
        {
            DestroyRegUi();
        }

        private void OnEmailDoesntExist(object[] args)
        {
            Events.CallRemote("CLIENT:SERVER::CREATE_ACCOUNT", _user_email, _user_password);
        }

        private void OnEmailExists(object[] args)
        {
            authWindow.ExecuteJs("document.dispatchEvent(new Event('EmailIsExist'))");
        }

        private void OnButtonRegClick(object[] args)
        {
            _user_email = args[0].ToString();
            _user_password = args[1].ToString();

            Events.CallRemote("CLIENT:SERVER::VERFICATION_EMAIL", _user_email);
        }

        public void DrawRegUi()
        {
            Chat.Show(false);
            Ui.DisplayRadar(false);
            Audio.StartAudioScene("CHARACTER_CHANGE_IN_SKY_SCENE");
            authWindow = new HtmlWindow("package://CEF/registration/registration.html");
            authWindow.Active = true;
            Cursor.ShowCursor(true, true);

            camera = RAGE.Game.Cam.CreateCameraWithParams(RAGE.Game.Misc.GetHashKey("DEFAULT_SCRIPTED_CAMERA"), 636.32336f, 1254.177f, 346.7224f, 0f, 0f, 153.33537f, 70.0f, true, 2);
            RAGE.Game.Cam.SetCamActive(camera, true);
            RAGE.Game.Cam.RenderScriptCams(true, false, 0, true, false, 0);
        }

        public void  DestroyRegUi()
        {
            authWindow.Destroy();

            RAGE.Game.Cam.DestroyCam(camera, true);
            RAGE.Game.Cam.RenderScriptCams(false, false, 0, true, false, 0);

            _authController = new AuthController(false);
        }
    }
}
