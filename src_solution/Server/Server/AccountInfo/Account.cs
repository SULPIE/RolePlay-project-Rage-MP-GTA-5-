

using GTANetworkAPI;
using System;

namespace Server.AccountInfo
{
    public class Account
    {
        private Player _player = null;

        private String _name;
        private int _id;
        private long _money;
        private int _admin_level;
        private int _level;
        private string _skin;
        private int _sex;
        private bool _is_player_renting_bike = false;
        private bool _is_player_in_showroom = false;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public long Money
        {
            get { return _money; }
            set { _money = value; }
        }

        public int AdminLevel
        {
            get { return _admin_level; }
            set { _admin_level = value; }
        }

        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        public Player player
        {
            get { return _player; }
            set { _player = value; }
        }

        public string Skin
        {
            get { return _skin; }
            set { _skin = value; }
        }

        public int Sex
        {
            get { return _sex; }
            set { _sex = value; }
        }

        public bool IsPlayerRentingBike
        {
            get { return _is_player_renting_bike; }
            set { _is_player_renting_bike = value; }
        }

        public bool IsPlayerInShowRoom
        {
            get { return _is_player_in_showroom; }
            set { _is_player_in_showroom = value; }
        }

        public Account(Player player)
        {
            _player = player;
        }

        public void Init(String name, int id, int money, int admin_level, int level, string skin, int sex)
        {
            _name = name;
            _id = id;
            _money = money;
            _admin_level = admin_level; 
            _level = level;
            _skin = skin;
            _sex = sex;
        }
    }
}
