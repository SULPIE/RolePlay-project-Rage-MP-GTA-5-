using GTANetworkAPI;
using Server.Vehicles.PersonalVehicles;

namespace Server.EventScripts
{
    public class PlayerEnterVehicle : Script
    {
        [ServerEvent(Event.PlayerEnterVehicle)]
        public void OnPlayerEnterVehicle(Player player, Vehicle vehicle, sbyte seat)
        {
            if(PersonalVehicleHolder.GetPersonalVehicle(vehicle) != null) { return; }
            vehicle.EngineStatus = false;
        }
    }
}
