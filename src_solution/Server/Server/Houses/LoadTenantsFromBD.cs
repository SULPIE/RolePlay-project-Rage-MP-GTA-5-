using GTANetworkAPI;
using MySql.Data.MySqlClient;
using Server.Data;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Server.Houses
{
    public class LoadTenantsFromBD
    {
        public static void Start(House house)
        {
            try
            {
                MySqlCommand command = new MySqlCommand("SELECT * FROM houses_tenants WHERE houseid=@houseid");
                command.Parameters.AddWithValue("@houseid", house.HouseID);
                DataTable dataTable = Query.ExecuteRead(command);

                if (dataTable.Rows.Count == 0) { return; }

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    int houseid = dataTable.Rows[i].Field<int>("houseid");
                    house.Tenants.Add(dataTable.Rows[i].Field<string>("name"));
                }
            }
            catch(Exception e)
            {
                NAPI.Util.ConsoleOutput(e.ToString());
            }
        }
    }
}
