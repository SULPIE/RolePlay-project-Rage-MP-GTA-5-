using GTANetworkAPI;

namespace Server.ServerCommands
{
    internal class Car : Script
    {
        [Command("getcar")]
        public void Create(Player player, string vehname, int color1, int color2)
        {
            uint vhash = NAPI.Util.GetHashKey(vehname);
            Vehicle veh = NAPI.Vehicle.CreateVehicle(vhash, player.Position, player.Heading, 1, 1);
            veh.Locked = false;
            veh.EngineStatus = true;
            player.SetIntoVehicle(veh, (int)VehicleSeat.Driver);
        }
    }
}
