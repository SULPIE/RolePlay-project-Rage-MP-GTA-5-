﻿
using GTANetworkAPI;

namespace Server.Vehicles.PersonalVehicles
{
    public class PersonalVehicle
    {
        private string _name;
        private string _owner;
        private int _health;
        private string _num;
        private int _first_color;
        private int _second_color;

        private Vector3 _start_position;
        private Vector3 _start_rotate;

        private Vehicle _vehicle = null;

        public string Name { get { return _name; } }
        public Vehicle Vehicle { get { return _vehicle; } }
        public string Owner { get { return _owner; } set { _owner = value; } }
        public int Health { get { return _health; } }
        public string Num { get { return _num; } set { _num = value; } }
        public int FirstColor { get { return _first_color; } set { _first_color = value; } }
        public int SecondColor { get { return _second_color; } set { _second_color = value; } }

        public PersonalVehicle(string name, string owner, int health, string num, int first_color, int second_color, Vector3 position, Vector3 rotation) 
        {
            _name = name;
            _owner = owner;
            _health = health;
            _num = num;
            _first_color = first_color;
            _second_color = second_color;

            uint vhash = NAPI.Util.GetHashKey(name);
            _vehicle = NAPI.Vehicle.CreateVehicle(vhash, position, 0f, first_color, second_color);
            _vehicle.Position = position;
            _vehicle.Rotation = rotation;
        }
    }
}