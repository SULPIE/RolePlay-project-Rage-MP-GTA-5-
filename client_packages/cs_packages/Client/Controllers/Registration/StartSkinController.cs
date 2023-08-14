using RAGE;
using RAGE.Elements;
using RAGE.Game;
using RAGE.Ui;
using System;
using System.Drawing;
using static System.String;

namespace Client.Controllers.Registration
{
    public class StartSkinController
    {
        private int _current_sex = 0;
        private int _current_skin = 0;

        private static HtmlWindow authWindow;
        private static int camera = 0;

        private readonly String[,] _skinName = new string[2, 14]
        {
            {"a_m_m_tramp_01", "ig_manuel", "a_m_y_methhead_01", "a_m_y_mexthug_01", "u_m_y_militarybum", "csb_ramp_hic", "ig_ramp_hipster", "ig_russiandrunk", "a_m_m_rurmeth_01", "a_m_o_salton_01", "a_m_o_tramp_01", "a_m_m_trampbeac_01", "a_m_y_vinewood_03", "a_f_m_skidrow_01"},
            {"s_f_y_shop_low", "a_f_y_scdressy_01", "ig_screen_writer", "a_f_y_rurmeth_01", "u_f_y_princess", "ig_patricia", "u_f_y_mistress", "s_f_y_migrant_01", "ig_maude", "u_f_y_hotposh_01", "a_f_y_hipster_02", "a_f_y_hiker_01", "cs_guadalope", "a_f_y_epsilon_01"}
        };
        public StartSkinController() 
        {
            Events.Add("CEF:CLIENT::ON_CHANGE_SEX", OnChangeSex);
            Events.Add("CEF:CLIENT::ON_CHANGE_SKIN", OnChangeSkin);
            Events.Add("CEF:CLIENT::ON_GETTING_NEXT", OnGettingNext);

            DrawSkinUi();
        }

        private void OnGettingNext(object[] args)
        {
            authWindow.Destroy();
            Cursor.ShowCursor(false, false);
            Chat.Show(true);
            Ui.DisplayRadar(true);

            RAGE.Game.Cam.DestroyCam(camera, true);
            RAGE.Game.Cam.RenderScriptCams(false, false, 0, true, false, 0);
        }

        public void DrawSkinUi()
        {
            authWindow = new HtmlWindow("package://CEF/character_customization/index.html");
            authWindow.Active = true;
            Cursor.ShowCursor(true, true);
            Chat.Show(false);
            Ui.DisplayRadar(false);

            camera = RAGE.Game.Cam.CreateCameraWithParams(Misc.GetHashKey("DEFAULT_SCRIPTED_CAMERA"), 348.87527f, -984.7415f, 29.357616f, 0, 0, 89.81727f, 70.0f, true, 2);
            RAGE.Game.Cam.SetCamActive(camera, true);
            RAGE.Game.Cam.RenderScriptCams(true, false, 0, true, false, 0);

            Events.CallRemote("CLIENT:SERVER::SET_POS", 345.89932f, -984.9822f, 29.35975f, 0f, 0f, -85.69115f);
        }

        private void OnChangeSkin(object[] args)
        {
            try
            {
                _current_skin = (int)args[0];

                Events.CallRemote("CLIENT:SERVER::SET_PLAYER_SKIN_INTRO", _skinName[_current_sex, _current_skin], _current_sex);
            }
            catch(Exception e) { }
        }

        private void OnChangeSex(object[] args)
        {
            try
            {
                _current_sex = (int)args[0];
                _current_skin = 0;

                Events.CallRemote("CLIENT:SERVER::SET_PLAYER_SKIN_INTRO", _skinName[_current_sex, _current_skin], _current_sex);
            }
            catch(Exception ex) { }
        }
    }
}
