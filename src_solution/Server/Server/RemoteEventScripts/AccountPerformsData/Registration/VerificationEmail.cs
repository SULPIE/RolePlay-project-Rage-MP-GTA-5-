using GTANetworkAPI;
using MySql.Data.MySqlClient;
using Server.Data;
using System.Data;

namespace Server.RemoteEventScripts.AccountPerformsData.Registration
{
    internal class VerificationEmail : Script
    {
        [RemoteEvent("CLIENT:SERVER::VERFICATION_EMAIL")]
        public async void OnVerificationEmail(Player player, string email)
        {
            string select_query = "SELECT * FROM users WHERE email=@email";

            MySqlCommand command = new MySqlCommand(select_query);
            command.Parameters.AddWithValue("@email", email);

            DataTable dataTable = await Query.ExecuteReadAsync(command);
            if (dataTable.Rows.Count > 0)
            {
                NAPI.Task.Run(() => NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::EMAIL_EXISTS"));
            }
            else
            {
                NAPI.Task.Run(() => NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::EMAIL_DOESNT_EXIST"));
            }
        }
    }
}
