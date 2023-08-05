using GTANetworkAPI;
using MySql.Data.MySqlClient;
using Server.Data;
using System;

namespace Server.AccountInfo
{
    public static class Update
    {
        public static async void Data(Player player)
        {
            string update_query = 
                "UPDATE users SET `money`=@money, " +
                "`level`=@level, " +
                "`admin_level`=@admin_level, " +
                "`skin_name`=@skin_name," +
                "`sex`=@sex" +
                " WHERE `name`=@name";

            try
            {
                MySqlCommand command = new MySqlCommand(update_query);

                command.Parameters.AddWithValue("@money", AccountHandlerDictionary.GetAccount(player).Money);
                command.Parameters.AddWithValue("@level", AccountHandlerDictionary.GetAccount(player).Level);
                command.Parameters.AddWithValue("@admin_level", AccountHandlerDictionary.GetAccount(player).AdminLevel);
                command.Parameters.AddWithValue("@skin_name", AccountHandlerDictionary.GetAccount(player).Skin);
                command.Parameters.AddWithValue("@sex", AccountHandlerDictionary.GetAccount(player).Sex);
                command.Parameters.AddWithValue("@name", AccountHandlerDictionary.GetAccount(player).Name);

                await Query.ExecuteReadAsync(command);
            }
            catch (Exception ex)
            {
                NAPI.Util.ConsoleOutput(ex.ToString());
            }
        }
    }
}
