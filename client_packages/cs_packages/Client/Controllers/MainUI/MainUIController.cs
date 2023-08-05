using RAGE;
using RAGE.Game;
using RAGE.Ui;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Controllers.MainUI
{
    public class MainUIController
    {
        HtmlWindow htmlWindow = new HtmlWindow("package://CEF/mainui/index.html");
        string prevStreet = "";
        long prevMoney = 0;
        public MainUIController() 
        {
            Events.Add("SERVER:CLIENT::ON_DRAW_UI", OnDrawUI);
        }

        private void OnDrawUI(object[] args)
        {
            var tempStreetName = 0;
            var tempCrossingRoad = 0;
            var tempResult = string.Empty;
            RAGE.Game.Pathfind.GetStreetNameAtCoord((float)args[1], (float)args[2], (float)args[3], ref tempStreetName, ref tempCrossingRoad);
            tempResult = RAGE.Game.Ui.GetStreetNameFromHashKey((uint)tempStreetName);

            if(tempResult == prevStreet && prevMoney == (long)args[0])
            {
                return;
            }

            htmlWindow.Active = true;
            htmlWindow.Call("CLIENT:CEF::DRAW_UI_DATA", tempResult, args[0].ToString());

            prevMoney = (long)args[0];
            prevStreet = tempResult;
        }
    }
}
