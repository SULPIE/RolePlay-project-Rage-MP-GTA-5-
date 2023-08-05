using GTANetworkAPI;
using Server.Vehicles.PersonalVehicles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.ServerCommands
{
    public class PersonCar : Script
    {
        [Command("addpersoncar")]
        public void CreateCar(Player player, string vehname, int color1, int color2, string num)
        {
            CreatePhysicalPersonVehicle.Start(player, color1, color2, num, player.Position, player.Rotation, vehname);
        }
    }
}
