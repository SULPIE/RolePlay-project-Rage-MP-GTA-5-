using GTANetworkAPI;
using Server.AccountInfo;

namespace Server.Vehicles.PersonalVehicles
{
    public static class CreatePhysicalPersonVehicle
    {
        public static void Start(Player player, int color1, int color2, string num, Vector3 position, Vector3 rotation, string vehname)
        {
            if(AccountHandlerDictionary.GetAccount(player) == null)
            {
                return;
            }

            PersonalVehicle vehicle = new PersonalVehicle(vehname, player.Name, 1000, "432OX0", color1, color2, position, rotation);
            InsertVehicle.Start(player, vehname, color1, color2, num, position, rotation);
        }
    }
}
