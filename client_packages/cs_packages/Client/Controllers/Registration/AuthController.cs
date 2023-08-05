using Client.Controllers.CarShowRoom;
using Client.Controllers.MainUI;
using RAGE;
using RAGE.Game;
using RAGE.NUI;
using RAGE.Ui;
using System;

namespace Client.Controllers.Registration
{
    internal class AuthController
    {
        private static HtmlWindow authWindow;
        private static int camera = 0;

        public bool IsPlayerRegistered = false;

        public AuthController(bool isPlayerRegistered)
        {
            Events.Add("CEF:CLIENT::ON_AUTH_BUTTON_CLICK", OnAuthButtonClick);
            Events.Add("SERVER:CLIENT::INVALID_LOGIN_DATA", OnUserTypeInvalidData);
            Events.Add("SERVER:CLIENT::CORRECT_LOGIN_DATA", OnUserLogon);

            DrawAuthUi();
            IsPlayerRegistered = isPlayerRegistered;
        }

        private void OnUserLogon(object[] args)
        {
            DestroyAuthUi();

            if (!IsPlayerRegistered)
            {
                StartSkinController startSkinController = new StartSkinController();
            }

            MainUIController mainUIController = new MainUIController();
            ShowRoomController showRoomController = new ShowRoomController();
        }

        private void OnUserTypeInvalidData(object[] args)
        {
            authWindow.ExecuteJs("document.dispatchEvent(new Event('DataIsInvalid'))");
        }

        private void DrawAuthUi()
        {
            Chat.Show(false);
            Ui.DisplayRadar(false);
            Audio.StartAudioScene("CHARACTER_CHANGE_IN_SKY_SCENE");
            authWindow = new HtmlWindow("package://CEF/registration/index.html");
            authWindow.Active = true;
            Cursor.ShowCursor(true, true);

            camera = RAGE.Game.Cam.CreateCameraWithParams(RAGE.Game.Misc.GetHashKey("DEFAULT_SCRIPTED_CAMERA"), 636.32336f, 1254.177f, 346.7224f, 0f, 0f, 153.33537f, 70.0f, true, 2);
            RAGE.Game.Cam.SetCamActive(camera, true);
            RAGE.Game.Cam.RenderScriptCams(true, false, 0, true, false, 0);

            //RAGE.Game.Ped.SetPedHeadBlendData(Player.PlayerPedId(), 44, 27, 0, 44, 2, 2, 1f, 1f, 0f, false);
        }

        private void DestroyAuthUi()
        {
            Chat.Show(true);
            Ui.DisplayRadar(true);
            authWindow.Destroy();
            Cursor.ShowCursor(false, false);


            RAGE.Game.Cam.DestroyCam(camera, true);
            RAGE.Game.Cam.RenderScriptCams(false, false, 0, true, false, 0);
        }

        private void OnAuthButtonClick(object[] args)
        {
            string email = args[0].ToString();
            string password = args[1].ToString();

            Events.CallRemote("CLIENT:SERVER::VERIFICATION_ON_LOGIN", email, password);
        }
    }
}
