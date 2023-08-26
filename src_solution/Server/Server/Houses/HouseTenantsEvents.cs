using GTANetworkAPI;
using Server.AccountInfo;
using System;

namespace Server.Houses
{
    internal class HouseTenantsEvents : Script
    {
        [RemoteEvent("CLIENT:SERVER::INITIALIZE_TENANTS_LIST")]
        public void InitializeTenantsList(Player player)
        {
            House player_house = null;

            foreach(ColShape house in NAPI.Pools.GetAllColShapes())
            {
                if (HousesHolder.GetHouse(house) == null) { continue; }

                if(HousesHolder.GetHouse(house).Owner == player.Name)
                {
                    player_house = HousesHolder.GetHouse(house);
                    break;
                }
            }

            if(player_house == null) 
            {
                NAPI.Util.ConsoleOutput("XUY");
                return; 
            }

            NAPI.Util.ConsoleOutput(player_house.Owner);

            for(int i = 0; i < player_house.Tenants.Count; i++)
            {
                NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::SEND_TENANT_LIST", player_house.Tenants[i]);
            }

            player.SetData<House>("player_house", player_house);
        }

        [RemoteEvent("CLIENT:SERVER::DELETE_TENANT")]
        public void DeleteTenants(Player player, string tenant_id)
        {
            if(player.GetData<House>("player_house") == null) { return; }
            if (AccountHandlerDictionary.GetAccount(player) == null) { return; };

            try
            {
                int tenant = Convert.ToInt32(tenant_id);
                House player_house = player.GetData<House>("player_house");

                if (player_house.Tenants[tenant] != null)
                {
                    player_house.Tenants.RemoveAt(tenant);
                }
            }
            catch(Exception e)
            {
                NAPI.Util.ConsoleOutput(e.ToString());
            }
        }
    }
}
