using GTANetworkAPI;
using MySql.Data.MySqlClient;
using Server.Data;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Server.Houses
{
    public static class LoadHousesFromDB
    {
        public static void Start()
        {
            try
            {
                MySqlCommand command = new MySqlCommand("SELECT * FROM houses");
                DataTable dataTable = Query.ExecuteRead(command);

                if (dataTable.Rows.Count == 0) { return; }

                NAPI.Task.Run(() =>
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        House house = new House();
                        house.Init
                        (
                            dataTable.Rows[i].Field<int>("id"),
                            dataTable.Rows[i].Field<int>("type"),
                            new Vector3(dataTable.Rows[i].Field<float>("posx"), dataTable.Rows[i].Field<float>("posy"), dataTable.Rows[i].Field<float>("posz")),
                            HouseTypesInfo.HouseTypesInfoArray[dataTable.Rows[i].Field<int>("type"), (int)HouseTypesInfo.HouseInteriorPositions.PlayerPosition],
                            HouseTypesInfo.HouseTypesInfoArray[dataTable.Rows[i].Field<int>("type"), (int)HouseTypesInfo.HouseInteriorPositions.ExitPickup],
                            dataTable.Rows[i].Field<int>("dimension"),
                            dataTable.Rows[i].Field<int>("cost"),
                            dataTable.Rows[i].Field<string>("owner")
                        );
                        LoadTenantsFromBD.Start(house);
                    }
                });

                NAPI.Util.ConsoleOutput($"{dataTable.Rows.Count} домов загружено");
            }
            catch(Exception e)
            {
                NAPI.Util.ConsoleOutput(e.ToString());
            }
        }
    }
}
