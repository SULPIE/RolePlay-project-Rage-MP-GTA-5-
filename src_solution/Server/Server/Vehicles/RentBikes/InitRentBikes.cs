
using GTANetworkAPI;

namespace Server.Vehicles.RentBikes
{
    public class InitRentBikes : Script
    {
        private float[,] bikes =
        {
            //Обычный спавн
            {429.40448f, -643.3166f, 28.07929f, -70f},
            {430.11572f, -644.43024f, 28.08431f, -70f },
            {430.08337f, -645.6951f, 28.083668f, -70f},
            {429.8358f, -646.9889f, 28.08249f, -70f },
            {430.30875f, -648.4743f, 28.081112f, -70f },
            {430.08548f, -650.9943f, 28.084196f, -70f },
            {429.3938f, -652.4824f, 28.079535f, -70f },
            {430.293f, -649.4771f, 28.08174f, -70.00929f },

            //Начальный спавн
            {343.38724f, -963.7855f, 29.013292f, 103.80318f },
            {342.38724f, -963.7855f, 29.013292f, 103.80318f },
            {341.38724f, -963.7855f, 29.013292f, 103.80318f },
            {340.38724f, -963.7855f, 29.013292f, 103.80318f }
        };

        private string bike_name = "cruiser";

        [ServerEvent(Event.ResourceStart)]
        public void Start()
        {
            for(int i = 0; i < bikes.GetLength(0); i++)
            {
                Vector3 position = new Vector3(bikes[i, 0], bikes[i, 1], bikes[i, 2]);
                Vector3 rotation = new Vector3(0f, 0f, bikes[i, 3]);

                RentBike bike = new RentBike(50, position, bike_name, rotation);
                RentBikesDictionary.AddBikeToDictionary(bike.Veh, bike);
            }

            NAPI.Util.ConsoleOutput($"Велопарк в количестве {bikes.GetLength(0)} штук успешно загружен.");
        }
    }
}
