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

        protected Vector3 _exit_position = new Vector3();
        protected Vector3 _exit_rotation = new Vector3();

        protected Vector3 _exit_colshape_cords = new Vector3();

        protected static readonly int _max_vehicles = 500;
        
        protected string[,] _cars_info;

        protected Marker marker;
        protected ColShape colShape;
        protected ColShape exitColShape;

        public Vector3 PlayerPosition { get { return _interior_cord_position; } }
        public Vector3 PlayerRotation { get { return _interior_cord_rotation; } }
        public Vector3 CarPosition { get { return _car_position; } }
        public Vector3 CarRotation { get { return _car_rotation; } }
        public string[,] CarInfoArray { get { return _cars_info; } }
        public Vector3 ExitPosition { get { return _exit_position; } }
        public Vector3 ExitRotation { get { return _exit_rotation; } }
        public ColShape EnterColshape { get { return colShape; } }
        public ColShape ExitColshape { get { return exitColShape; } }

        public void Init(string[,] cars, Vector3 interior_cord_position, Vector3 interior_cord_rotation, Vector3 marker_position, Vector3 car_position, Vector3 car_rotation, Vector3 exit_position, Vector3 exit_rotation, Vector3 exit_colshape_pos) 
        {
            try
            {
                _cars_info = cars;
                _interior_cord_position = interior_cord_position;
                _interior_cord_rotation = interior_cord_rotation;
                _car_position = car_position;
                _car_rotation = car_rotation;
                _marker_position = marker_position;
                _exit_position = exit_position;
                _exit_rotation = exit_rotation;

                marker_position.Z -= 1.0f;
                exit_colshape_pos.Z -= 1.0f;
                marker = NAPI.Marker.CreateMarker(MarkerType.VerticalCylinder, marker_position, new Vector3(), new Vector3(), 1.0f, new Color(255, 255, 255));
                NAPI.Marker.CreateMarker(MarkerType.VerticalCylinder, exit_colshape_pos, new Vector3(), new Vector3(), 1.0f, new Color(255, 255, 255));
                Blip blip = NAPI.Blip.CreateBlip(810, marker_position, 1.0f, 54);
                NAPI.Blip.SetBlipShortRange(blip, true);
                colShape = NAPI.ColShape.CreateCylinderColShape(marker_position, 1.0f, 1.0f);
                exitColShape = NAPI.ColShape.CreateCylinderColShape(exit_colshape_pos, 1.0f, 1.0f);

                ShowRoomHolder.AddShowRoomToHolder(colShape, this);
            }
            catch(Exception e)
            {
                NAPI.Util.ConsoleOutput(e.ToString());
            }
        }
    }
}
