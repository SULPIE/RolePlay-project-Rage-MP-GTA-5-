using GTANetworkAPI;
using MySql.Data.MySqlClient;
using System;

namespace Server.Data
{
    public class ContextData
    {
        public static readonly string connection = "server=localhost;user=root;database=ragedb;password=;Pooling=true;";

        public static void Init()
        {
            if (connection != null) 
            {
                using(MySqlConnection conn = new MySqlConnection(connection))
                {
                    try
                    {
                        conn.Open();
                        NAPI.Util.ConsoleOutput("Подключение к базе данных выполнено успешно");
                    }
                    catch (Exception e)
                    {
                        NAPI.Util.ConsoleOutput(e.ToString());
                    }
                }
            }
        }
    }
}
