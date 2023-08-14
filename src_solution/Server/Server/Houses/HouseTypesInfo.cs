using GTANetworkAPI;
using System.Runtime.Serialization;

namespace Server.Houses
{
    public static class HouseTypesInfo
    {
        public enum HouseType
        {
            Eco = 0
        }

        public enum HouseInteriorPositions
        {
            ExitPickup = 0,
            PlayerPosition = 1
        }

        public static readonly Vector3[,] HouseTypesInfoArray =
        {
            {new Vector3(266.1396f, -1007.40686f, -101.01582f), new Vector3(261.4586f, -998.8196f, -99.00863f)},
        };

        public static readonly string[] NameOfTypes =
        {
            "Эконом"
        };
    }
}
