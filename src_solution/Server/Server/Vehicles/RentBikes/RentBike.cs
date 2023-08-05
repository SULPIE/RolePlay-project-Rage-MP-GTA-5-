
using GTANetworkAPI;

namespace Server.Vehicles.RentBikes
{
    public class RentBike
    {
        private Player _client = null;
        private int _rent_cost = 0;
        private string _vehame = "";

        private Vehicle veh = null;

        public Vehicle Veh
        {
            get { return veh; }
        }

        public Player Client { get { return _client; } set { _client = value; } }

        private Vector3 _position = null;
        private Vector3 _rotation = null;

        public Vector3 Position { get { return _position; } set { _position = value; } }
        public Vector3 Rotation { get { return _rotation; } set { _rotation = value; } }

        public RentBike(int rent_cost, Vector3 position, string vehname, Vector3 rot)
        {
            _rent_cost = rent_cost;
            _position = position;
            _rotation = rot;

            uint vhash = NAPI.Util.GetHashKey(vehname);
            veh = NAPI.Vehicle.CreateVehicle(vhash, position, 0f, 1, 1);
            veh.Rotation = rot;
            veh.Locked = false;
            veh.EngineStatus = false;
        }
    }
}
