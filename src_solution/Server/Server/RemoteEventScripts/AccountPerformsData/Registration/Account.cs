using GTANetworkAPI;
using MySql.Data.MySqlClient;
using Server.Data;
using System;

namespace Server.RemoteEventScripts.AccountPerformsData.Registration
{
    internal class Account : Script
    {
        [RemoteEvent("CLIENT:SERVER::CREATE_ACCOUNT")]
        public void Create(Player player, string email, string password)
        {
            string select_query = "INSERT INTO users (name, password, email) VALUES (@name, @password, @email)";

            try
            {
                MySqlCommand command = new MySqlCommand(select_query);
                command.Parameters.AddWithValue("@name", player.Name);
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@email", email);

                Query.Execute(command);

                NAPI.Task.Run(() => NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::ON_ACCOUNT_CREATE"));
            }
            catch(Exception ex) 
            {
                NAPI.Util.ConsoleOutput(ex.ToString());
            }
        }
    }
}
