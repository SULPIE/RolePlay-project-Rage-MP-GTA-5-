using GTANetworkAPI;
using MySql.Data.MySqlClient;
using Server.Data;
using System;

namespace Server.RemoteEventScripts.AccountPerformsData.Registration
{
    internal class AccountEvent : Script
    {
        [RemoteEvent("CLIENT:SERVER::CREATE_ACCOUNT")]
        public async void Create(Player player, string email, string password)
        {
            //player.SetFaceFeature
            //player.SetCustomization
            string select_query = "INSERT INTO users (name, password, email) VALUES (@name, @password, @email)";

            try
            {
                MySqlCommand command = new MySqlCommand(select_query);
                command.Parameters.AddWithValue("@name", player.Name);
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@email", email);

                await Query.Execute(command);

                NAPI.Task.Run(() => NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::ON_ACCOUNT_CREATE"));
            }
            catch(Exception ex) 
            {
                NAPI.Task.Run(() => NAPI.Util.ConsoleOutput(ex.ToString()));
            }
        }
    }
}
