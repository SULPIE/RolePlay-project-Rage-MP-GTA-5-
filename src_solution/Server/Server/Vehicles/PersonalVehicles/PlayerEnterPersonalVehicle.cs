using GTANetworkAPI;

namespace Server.Vehicles.PersonalVehicles
{
    public class PlayerEnterPersonalVehicle : Script
    {
        [ServerEvent(Event.PlayerEnterVehicle)]
        public void OnPlayerEnterPersonalVCehicle(Player player, Vehicle vehicle, sbyte seat)
        {
            if(PersonalVehicleHolder.GetPersonalVehicle(vehicle) == null) { return; }
            vehicle.EngineStatus = PersonalVehicleHolder.GetPersonalVehicle(vehicle).EngineStatus;
        }
    }
}
