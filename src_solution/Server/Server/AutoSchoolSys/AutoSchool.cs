using GTANetworkAPI;

namespace Server.AutoSchoolSys
{
    public class AutoSchool
    {
        private readonly Vector3 _enter_pickup = Util.EnterPickupPosition;
        private readonly Vector3 _exit_pickup = Util.ExitPickupPosition;
        private ColShape _enter_colshape;
        private ColShape _exit_colshape;
        private ColShape[] _examine_pickups;

        public AutoSchool() 
        { 

        }
    }
}
