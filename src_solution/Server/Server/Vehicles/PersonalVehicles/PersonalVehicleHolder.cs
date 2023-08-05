using GTANetworkAPI;
using Server.AccountInfo;
using System.Collections.Generic;

namespace Server.Vehicles.PersonalVehicles
{
    public class PersonalVehicleHolder
    {
        public static Dictionary<Vehicle, PersonalVehicle> personal_cars = new Dictionary<Vehicle, PersonalVehicle>();

        public static PersonalVehicle GetPersonalVehicle(Vehicle vehicle)
        {
            if (personal_cars.ContainsKey(vehicle))
            {
                return personal_cars[vehicle];
            }
            else
            {
                return null;
            }
        }

        public static void AddPrivateVehicleToHandler(Vehicle vehicle, PersonalVehicle personalVehicle)
        {
            personal_cars.Add(vehicle, personalVehicle);
        }

        public static int DictionarySize()
        {
            return (int)personal_cars.Count;
        }
    }
}
