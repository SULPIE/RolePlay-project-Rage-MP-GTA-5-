using GTANetworkAPI;
using MySql.Data.MySqlClient;
using Server.Data;
using System.Data;

namespace Server.RemoteEventScripts.AccountPerformsData.Registration
{
    public class VerificationOnLogin : Script
    {
        [RemoteEvent("CLIENT:SERVER::VERIFICATION_ON_LOGIN")]
        public async void OnLoginAccountVerification(Player player, string email, string password)
        {
            string select_query = "SELECT * FROM users WHERE name=@name AND email=@email AND password=@password";

            MySqlCommand command = new MySqlCommand(select_query);
            command.Parameters.AddWithValue("@name", player.Name);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@password", password);

            DataTable dataTable = await Query.ExecuteReadAsync(command);

            if(dataTable.Rows.Count == 0)
            {
                NAPI.Task.Run(() => NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::INVALID_LOGIN_DATA"));
            }
            else
            {
                NAPI.Task.Run(() => NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::CORRECT_LOGIN_DATA"));
            }
        }
    }
}
