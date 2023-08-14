using GTANetworkAPI;
using Server.AccountInfo;
using System.Threading.Tasks;

namespace Server.Houses
{
    public class HouseServerEvents : Script
    {
        [ServerEvent(Event.ResourceStart)]
        public async Task InitHouses()
        {
            await LoadHousesFromDB.Start();
        }

        [ServerEvent(Event.PlayerEnterColshape)]
        public void OnPlayerEnterHouseEnterColshape(ColShape colShape, Player player)
        {
            House house = HousesHolder.GetHouse(colShape);

            if (house.ColShapeEnter != colShape) { return; }
            if (AccountHandlerDictionary.GetAccount(player) == null) { return; }

            string house_status = (house.Owner == null) ? "на продаже" : "куплен";

            NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::ON_PLAYER_PICKUP_HOUSE_PICKUP", HouseTypesInfo.NameOfTypes[house.HouseType], house.HouseID.ToString(), house.Cost.ToString(), house.Owner, house_status);
            player.SetData<ColShape>("house_colshape_player_enter_in", colShape);
        }

        [ServerEvent(Event.PlayerExitColshape)]
        public void OnPlayerExitHouseEnterColshape(ColShape colShape, Player player)
        {
            if (AccountHandlerDictionary.GetAccount(player) == null) { return; }

            if (HousesHolder.GetHouse(colShape).ColShapeEnter == colShape)
            {
                NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::ON_PLAYER_EXIT_HOUSE_PICKUP");
            }
        }

        [ServerEvent(Event.PlayerEnterColshape)]
        public void OnPlayerEnterHouseExitColshape(ColShape colShape, Player player)
        {
            if (AccountHandlerDictionary.GetAccount(player) == null) { return; }

            if (HousesHolder.GetHouse(colShape).ColShapeExit == colShape)
            {
                player.Position = HousesHolder.GetHouse(colShape).PickupPosition;
                player.Dimension = (uint)HousesHolder.GetHouse(colShape).Dimension;
            }
        }

        [RemoteEvent("CLIENT:SERVER::ON_PLAYER_PRESSED_TO_ENTER_HOUSE")]
        public void OnPlayerPressedToEnterHouse(Player player)
        {
            if (player.GetData<ColShape>("house_colshape_player_enter_in") == null) { return; }
            ColShape colShape = player.GetData<ColShape>("house_colshape_player_enter_in");

            House house = HousesHolder.GetHouse(colShape);

            if (house == null) { return; }
            if (house.ColShapeEnter != colShape) { return; }
            if (house.Owner == null) { return; }

            if(house.Owner == null)
            {
                NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::ON_PLAYER_BUY_HOUSE", house.Cost.ToString(), HouseTypesInfo.NameOfTypes[house.HouseType]);
            }
            else
            {
                player.Position = house.PlayerPositionInInterior;
                player.Dimension = (uint)house.HouseID;

                NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::ON_PLAYER_EXIT_HOUSE_PICKUP");
            }
        }

        [RemoteEvent("ON_PLAYER_CONFIRM_ON_BUY")]
        public void OnPlayerConfirmOnBuy(Player player)
        {
            if(AccountHandlerDictionary.GetAccount(player) == null) { return; }

            if (player.GetData<ColShape>("house_colshape_player_enter_in") == null) { return; }
            ColShape colShape = player.GetData<ColShape>("house_colshape_player_enter_in");

            House house = HousesHolder.GetHouse(colShape);
            if(house.Owner != null) { return; }

            if (AccountHandlerDictionary.GetAccount(player).Money < house.Cost)
            {
                player.SendChatMessage("~r~[Ошибка]~w~:У вас недостаточно денег для покупки квартиры.");
                return;
            }

            MoneyTransaction.TakeMoney(AccountHandlerDictionary.GetAccount(player), house.Cost);

            player.SendChatMessage("Вы успешно приобрели дом " + HouseTypesInfo.NameOfTypes[house.HouseType]);
            house.Owner = player.Name;

            NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::ON_PLAYER_EXIT_HOUSE_PICKUP");
        }
    }
}
