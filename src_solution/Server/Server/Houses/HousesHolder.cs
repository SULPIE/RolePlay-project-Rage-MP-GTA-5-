using GTANetworkAPI;
using Server.car_showroom;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Server.Houses
{
    public static class HousesHolder
    {
        public static Dictionary<ColShape, House> house_colshapes = new Dictionary<ColShape, House>();

        public static House GetHouse(ColShape colShape)
        {
            if (house_colshapes.ContainsKey(colShape))
            {
                return house_colshapes[colShape];
            }
            else
            {
                return null;
            }
        }

        public static void AddHouseToHolder(ColShape colShape, House house)
        {
            house_colshapes.Add(colShape, house);
        }
    }
}
