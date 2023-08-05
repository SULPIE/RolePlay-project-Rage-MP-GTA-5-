using GTANetworkAPI;
using System.Collections.Generic;

namespace Server.Vehicles.RentBikes
{
    public static class RentBikesDictionary
    {
        private static Dictionary<Vehicle, RentBike> rent_bikes = new Dictionary<Vehicle, RentBike>();

        public static RentBike GetRentBike(Vehicle vehicle)
        {
            try
            {
                if (rent_bikes.ContainsKey(vehicle))
                {
                    return rent_bikes[vehicle];
                }
                else
                {
                    return null;
                }
            }
            catch { return null; }
        }

        public static void AddBikeToDictionary(Vehicle vehicle, RentBike rentBike)
        {
            rent_bikes.Add(vehicle, rentBike);
        }
    }
}
