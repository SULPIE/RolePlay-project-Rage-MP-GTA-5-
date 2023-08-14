using RAGE;

namespace Client.Controllers.PlayerStateController
{
    public class FreezeController : Events.Script
    {
        public FreezeController()
        {
            Events.Add("SERVER:CLIENT::PLAYER_VEHICLE_FREEZE", FreezePlayerVehicle);
            Events.Add("SERVER:CLIENT::PLAYER_VEHICLE_UNFREEZE", UnFreezePlayerVehicle);
        }

        private void UnFreezePlayerVehicle(object[] args)
        {
            RAGE.Elements.Player.LocalPlayer.Vehicle.FreezePosition(false);
        }

        private void FreezePlayerVehicle(object[] args)
        {
            RAGE.Elements.Player.LocalPlayer.Vehicle.FreezePosition(true);
        }
    }
}
