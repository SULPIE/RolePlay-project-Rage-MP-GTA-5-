using GTANetworkAPI;
using Server.AccountInfo;
using System;
using System.Threading;

namespace Server.Vehicles.RentBikes
{
    internal class RentBikeEvent : Script
    {

        [ServerEvent(Event.PlayerEnterVehicle)]
        public void OnSitRentBike(Player player, Vehicle vehicle, sbyte seat)
        {
            if(RentBikesDictionary.GetRentBike(vehicle) == null)
            {
                return;
            }

            Timer stateTimer = player.GetData<Timer>("player_rentbike_timer_exit");

            if(stateTimer != null && RentBikesDictionary.GetRentBike(vehicle).Client == player)
            {
                stateTimer.Dispose();
            }

            RentBike rentBike = RentBikesDictionary.GetRentBike(vehicle);

            if (rentBike == null)
            {
                return;
            }

            if (rentBike.Client == null && AccountHandlerDictionary.GetAccount(player).IsPlayerRentingBike == false)
            {
                player.SendChatMessage("~g~[Велопарк]~w~:~w~Аренда велосипеда стоит ~r~50$");
                player.SendChatMessage("~g~[Велопарк]~w~:~w~Чтобы арендовать, введите команду ~r~/rentbike");

                NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::PLAYER_VEHICLE_FREEZE");
            }
            else if(rentBike.Client != player)
            {
                player.SendChatMessage("~r~[Ошибка]~w~:Вы уже арендуете велосипед!");
                player.WarpOutOfVehicle();
                return;
            }

            if(rentBike.Client != null && rentBike.Client != player)
            {
                player.WarpOutOfVehicle();
                player.SendChatMessage("~r~[Ошибка]~w~:Этот велосипед уже арендован!");
                return;
            }
        }

        [ServerEvent(Event.PlayerExitVehicle)]
        public void OnExitRentBike(Player player, Vehicle vehicle)
        {
            if(RentBikesDictionary.GetRentBike(vehicle) != null && RentBikesDictionary.GetRentBike(vehicle).Client == player)
            {
                player.SendChatMessage("~g~[Велопарк]~w~:У вас есть ~r~4 минуты~w~, чтобы сесть в свой велосипед!");
                Timer stateTimer = new Timer(ResetDeal, player, 9000, 250);
                player.SetData<Timer>("player_rentbike_timer_exit", stateTimer);
            }
        }

        private void ResetDeal(object state)
        {
            Player player = (Player)state;
            Vehicle vehicle = player.GetData<Vehicle>("player_renting_bike");
            Timer stateTimer = player.GetData<Timer>("player_rentbike_timer_exit");

            NAPI.Task.Run(() => {
                vehicle.Position = RentBikesDictionary.GetRentBike(vehicle).Position;
                vehicle.Rotation = RentBikesDictionary.GetRentBike(vehicle).Rotation;
                player.SendChatMessage("~g~[Велопарк]~w~:Ваш велосипед возвращен на базу!");
            });

            if (stateTimer != null)
            {
                stateTimer.Dispose();
            }

            RentBikesDictionary.GetRentBike(vehicle).Client = null;
            AccountHandlerDictionary.GetAccount(player).IsPlayerRentingBike = false;
        }

        [ServerEvent(Event.PlayerDisconnected)]
        public void OnPlayerDisconnect(Player player, DisconnectionType type, string reason)
        {
            if(player == null || AccountHandlerDictionary.GetAccount(player) == null)
            {
                return;
            }

            if(AccountHandlerDictionary.GetAccount(player).IsPlayerRentingBike == false)
            {
                return;
            }

            Vehicle vehicle = player.GetData<Vehicle>("player_renting_bike");
            if(vehicle == null) { return; }

            vehicle.Position = RentBikesDictionary.GetRentBike(vehicle).Position;
            vehicle.Rotation = RentBikesDictionary.GetRentBike(vehicle).Rotation;
            RentBikesDictionary.GetRentBike(vehicle).Client = null;
        }
    }
}
