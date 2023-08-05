using GTANetworkAPI;
using System.Collections.Generic;

namespace Server.car_showroom
{
    public class ShowRoomHolder
    {
        public static Dictionary<ColShape, CarAutoShowRoom> show_room_colshapes = new Dictionary<ColShape, CarAutoShowRoom> ();

        public static CarAutoShowRoom GetShowRoom(ColShape colShape)
        {
            if (show_room_colshapes.ContainsKey(colShape))
            {
                return show_room_colshapes[colShape];
            }
            else
            {
                return null;
            }
        }

        public static void AddShowRoomToHolder(ColShape colShape, CarAutoShowRoom showRoom)
        {
            show_room_colshapes.Add(colShape, showRoom);
        }
    }
}
