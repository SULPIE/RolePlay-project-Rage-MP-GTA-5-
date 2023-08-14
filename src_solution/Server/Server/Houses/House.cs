using GTANetworkAPI;

namespace Server.Houses
{
    public class House
    {
        private Vector3 _pickup_position;
        private Vector3 _interior_position;
        private Vector3 _exit_pickup_position;

        private int _house_type;
        private int _house_id;
        private int _dimension;
        private int _cost;

        private string _owner;

        private ColShape _colshape_enter;
        private ColShape _colshape_exit;
        private Marker _marker_enter;
        private Marker _marker_exit;

        public ColShape ColShapeEnter { get { return _colshape_enter; } }
        public ColShape ColShapeExit { get { return _colshape_exit; } }
        public Vector3 PlayerPositionInInterior { get { return _interior_position; } }
        public int Dimension { get { return _dimension; } }
        public int HouseID { get { return _house_id; } }
        public int HouseType { get { return _house_type; } }
        public int Cost { get { return _cost; } }
        public Vector3 PickupPosition { get { return _pickup_position; } }
        public string Owner { get { return _owner; } set { _owner = value; } }

        public void Init(int house_id, int house_type, Vector3 pickup_position, Vector3 interior_position, Vector3 exit_pickup_position, int dimension, int cost, string owner)
        {
            _house_id = house_id;
            _house_type = house_type;
            _pickup_position = pickup_position;
            _interior_position = interior_position;
            _exit_pickup_position = exit_pickup_position;
            _dimension = dimension;
            _cost = cost;
            _owner = owner;

            CreateHouse();
        }

        private void CreateHouse()
        {
            _pickup_position.Z -= 1.0f;

            _colshape_enter = NAPI.ColShape.CreateCylinderColShape(_pickup_position, 2.0f, 1.0f);
            _marker_enter = NAPI.Marker.CreateMarker(MarkerType.VerticalCylinder, _pickup_position, new Vector3(), new Vector3(), 1.0f, new Color(255, 255, 255));
            _colshape_enter.Dimension = (uint)_dimension;
            _marker_enter.Dimension = (uint)_dimension;

            _colshape_exit = NAPI.ColShape.CreateCylinderColShape(_exit_pickup_position, 2.0f, 1.0f);
            _marker_exit = NAPI.Marker.CreateMarker(MarkerType.VerticalCylinder, _exit_pickup_position, new Vector3(), new Vector3(), 1.0f, new Color(255, 255, 255));
            _colshape_exit.Dimension = (uint)_house_id;
            _marker_exit.Dimension = (uint)_house_id;

            HousesHolder.AddHouseToHolder(_colshape_enter, this);
        }
    }
}
