using GTANetworkAPI;
using MySql.Data.MySqlClient;
using Server.Data;
using System;
using System.Data;

namespace Server.Vehicles.PersonalVehicles
{
    public class InitPersonalCars
    {
        public static async void CreateVehicle(string playerName)
        {
            foreach (Vehicle v in NAPI.Pools.GetAllVehicles())
            {
                if(PersonalVehicleHolder.GetPersonalVehicle(v) == null)
                {
                    continue;
                }

                if(PersonalVehicleHolder.GetPersonalVehicle(v).Owner == playerName)
                {
                    return;
                }
            }

            MySqlCommand command = new MySqlCommand("SELECT * FROM vehicles WHERE owner=@owner");
            command.Parameters.AddWithValue("@owner", playerName);

            DataTable dataTable = await Query.ExecuteReadAsync(command);

            if(dataTable.Rows.Count == 0)
            {
                return;
            }

            NAPI.Task.Run(() =>
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Vector3 position = new Vector3(dataTable.Rows[i].Field<float>("posx"),
                        dataTable.Rows[i].Field<float>("posy"),
                        dataTable.Rows[i].Field<float>("posz"));

                    Vector3 rotation = new Vector3(dataTable.Rows[i].Field<float>("rotx"),
                        dataTable.Rows[i].Field<float>("roty"),
                        dataTable.Rows[i].Field<float>("rotz"));

                    PersonalVehicle vehicle = new PersonalVehicle
                    (
                        dataTable.Rows[i].Field<string>("name"),
                        dataTable.Rows[i].Field<string>("owner"),
                        dataTable.Rows[i].Field<int>("health"),
                        dataTable.Rows[i].Field<string>("num"),
                        dataTable.Rows[i].Field<int>("first_color"),
                        dataTable.Rows[i].Field<int>("second_color"),
                        position,
                        rotation
                    );
                    PersonalVehicleHolder.AddPrivateVehicleToHandler(vehicle.Vehicle, vehicle);
                }
            });
        }
    }
}
