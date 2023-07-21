using GTANetworkAPI;
using MySql.Data.MySqlClient;
using Server.Data;
using System.Data;
using System.Threading.Tasks;

namespace Server.RemoteEventScripts.AccountPerformsData.Registration
{
    public class CheckAccount : Script
    {
        private bool isAccountExist;

        [RemoteEvent("CLIENT:SERVER::CheckAccountExisting")]
        public async void IsExistAsync(Player player)
        {
            string select_query = "SELECT * FROM users WHERE name=@name";

            MySqlCommand command = new MySqlCommand(select_query);
            command.Parameters.AddWithValue("@name", player.Name);

            DataTable dataTable = await Query.ExecuteReadAsync(command);
            if (dataTable.Rows.Count > 0)
            {
                isAccountExist = true;
            }
            else
            {
                isAccountExist = false;
            }

            NAPI.Task.Run(() => NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::USER_EXISTING", isAccountExist));
        }
    }
}
