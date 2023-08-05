using GTANetworkAPI;
using System;

namespace Server.car_showroom
{
    public abstract class CarAutoShowRoom
    {
        protected Vector3 _interior_cord_position = new Vector3();
        protected Vector3 _interior_cord_rotation = new Vector3();
        protected Vector3 _car_position = new Vector3();
        protected Vector3 _car_rotation = new Vector3();

        protected Vector3 _marker_position = new Vector3();

        protected static readonly int _max_vehicles = 500;
        
        protected string[,] _cars_info;

        protected Marker marker;
        protected ColShape colShape;

        public Vector3 PlayerPosition { get { return _interior_cord_position; } }
        public Vector3 PlayerRotation { get { return _interior_cord_rotation; } }
        public Vector3 CarPosition { get { return _car_position; } }
        public Vector3 CarRotation { get { return _car_rotation; } }
        public string[,] CarInfoArray { get { return _cars_info; } }

        public void Init(string[,] cars, Vector3 interior_cord_position, Vector3 interior_cord_rotation, Vector3 marker_position, Vector3 car_position, Vector3 car_rotation) 
        {
            try
            {
                _cars_info = cars;
                _interior_cord_position = interior_cord_position;
                _interior_cord_rotation = interior_cord_rotation;
                _car_position = car_position;
                _car_rotation = car_rotation;
                _marker_position = marker_position;

                marker_position.Z -= 1.0f;
                marker = NAPI.Marker.CreateMarker(MarkerType.VerticalCylinder, marker_position, new Vector3(), new Vector3(), 1.0f, new Color(255, 255, 255));
                Blip blip = NAPI.Blip.CreateBlip(810, marker_position, 1.0f, 54);
                NAPI.Blip.SetBlipShortRange(blip, true);
                colShape = NAPI.ColShape.CreateCylinderColShape(marker_position, 1.0f, 1.0f);

                ShowRoomHolder.AddShowRoomToHolder(colShape, this);
            }
            catch(Exception e)
            {
                NAPI.Util.ConsoleOutput(e.ToString());
            }
        }
    }
}
