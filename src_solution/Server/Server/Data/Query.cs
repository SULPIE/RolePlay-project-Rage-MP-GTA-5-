using GTANetworkAPI;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace Server.Data
{
    public class Query
    {
        public static async Task Execute(MySqlCommand command)
        {
            using(MySqlConnection conn =  new MySqlConnection(ContextData.connection))
            {
                try
                {
                    conn.Open();
                    command.Connection = conn;
                    await command.ExecuteNonQueryAsync();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    NAPI.Util.ConsoleOutput(ex.ToString());
                }
            }
        }

        public static DataTable ExecuteRead(MySqlCommand command)
        {
            using(MySqlConnection conn = new MySqlConnection(ContextData.connection))
            {
                try
                {
                    conn.Open();
                    command.Connection = conn;

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        using (DataTable dt = new DataTable())
                        {
                            dt.Load(reader);
                            return dt;
                        }
                    }
                }
                catch(Exception ex)
                {
                    NAPI.Util.ConsoleOutput(ex.ToString());
                    return null;
                }
            }
        }

        public async static Task<DataTable> ExecuteReadAsync(MySqlCommand command)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ContextData.connection))
                {
                    await conn.OpenAsync();
                    command.Connection = conn;

                    using (DbDataReader reader = await command.ExecuteReaderAsync())
                    {
                        using (DataTable dt = new DataTable())
                        {
                            dt.Load(reader);
                            return dt;
                        }
                    }
                }
            }
            catch(MySqlException ex)
            {
                NAPI.Util.ConsoleOutput(ex.ToString());
                return null;
            }
        }
    }
}
