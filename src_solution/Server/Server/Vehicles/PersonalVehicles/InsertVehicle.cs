using GTANetworkAPI;
using MySql.Data.MySqlClient;
using Server.Data;

namespace Server.Vehicles.PersonalVehicles
{
    public static class InsertVehicle
    {
        public static async void Start(Player player, string vehname, int color1, int color2, string num, Vector3 position, Vector3 rotation)
        {
            MySqlCommand command = new MySqlCommand
            (
            "INSERT INTO vehicles" +
            "(name, num, owner, first_color, second_color, posx, posy, posz, rotx, roty, rotz) VALUES " +
            "(@name, @num, @owner, @first_color, @second_color, @posx, @posy, @posz, @rotx, @roty, @rotz)");
            command.Parameters.AddWithValue("@name", vehname);
            command.Parameters.AddWithValue("@num", num);
            command.Parameters.AddWithValue("@owner", player.Name);
            command.Parameters.AddWithValue("@first_color", color1);
            command.Parameters.AddWithValue("@second_color", color2);
            command.Parameters.AddWithValue("@posx", position.X);
            command.Parameters.AddWithValue("@posy", position.Y);
            command.Parameters.AddWithValue("@posz", position.Z);
            command.Parameters.AddWithValue("@rotx", rotation.X);
            command.Parameters.AddWithValue("@roty", rotation.Y);
            command.Parameters.AddWithValue("@rotz", rotation.Z);

            await Query.Execute(command);
        }
    }
}
