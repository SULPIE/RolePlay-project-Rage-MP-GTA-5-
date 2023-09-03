using GTANetworkAPI;
using System.Collections.Generic;

namespace Server.AutoSchoolSys
{
    public class ExamineCarHolder
    {
        public static List<Vehicle> vehicles = new List<Vehicle>();

        public static void InitExamineVehicles(Vector3[,] car_array, string car_model)
        {
            for(int i = 0; i < car_array.GetLength(0); i++)
            {
                uint vhash = NAPI.Util.GetHashKey(car_model);
                Vehicle vehicle = NAPI.Vehicle.CreateVehicle(vhash, car_array[i, 0], car_array[i, 1].Z, 150, 150, "school");
                vehicles.Add(vehicle);
            }
        }
    
        public static Vehicle GetExamineCar(Vehicle vehicle)
        {
            return vehicles.Find(item => item == vehicle);
        }
    }
}
