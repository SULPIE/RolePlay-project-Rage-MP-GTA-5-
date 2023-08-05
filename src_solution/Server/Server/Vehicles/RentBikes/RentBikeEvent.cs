using GTANetworkAPI;
using Server.AccountInfo;
using System;
using System.Threading;

namespace Server.Vehicles.RentBikes
{
    internal class RentBikeEvent : Script
    {
        private Vehicle _vehicle = null;
        private Player _player = null;

        private Timer stateTimer = null;

        [ServerEvent(Event.PlayerEnterVehicle)]
        public void OnSitRentBike(Player player, Vehicle vehicle, sbyte seat)
        {
            if(RentBikesDictionary.GetRentBike(vehicle) == null)
            {
                return;
            }

            _vehicle = vehicle;
            _player = player;

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
            }
            else
            {
                player.SendChatMessage("~r~[Ошибка]~w~:Вы уже арендуете велосипед!");
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
            _vehicle = vehicle;
            _player = player;
            if(RentBikesDictionary.GetRentBike(vehicle) != null && RentBikesDictionary.GetRentBike(vehicle).Client == player)
            {
                player.SendChatMessage("~g~[Велопарк]~w~:У вас есть ~r~4 минуты~w~, чтобы сесть в свой велосипед!");
                stateTimer = new Timer(ResetDeal, 0, 240000, 250);
            }
        }

        private void ResetDeal(object state)
        {
            NAPI.Task.Run(() => {
                _vehicle.Position = RentBikesDictionary.GetRentBike(_vehicle).Position;
                _vehicle.Rotation = RentBikesDictionary.GetRentBike(_vehicle).Rotation;
                _player.SendChatMessage("~g~[Велопарк]~w~:Ваш велосипед возвращен на базу!");
            });
            if (stateTimer != null)
            {
                stateTimer.Dispose();
            }

            RentBikesDictionary.GetRentBike(_vehicle).Client = null;
            AccountHandlerDictionary.GetAccount(_player).IsPlayerRentingBike = false;
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

            if(stateTimer != null)
            {
                stateTimer.Dispose();
            }

            _vehicle.Position = RentBikesDictionary.GetRentBike(_vehicle).Position;
            _vehicle.Rotation = RentBikesDictionary.GetRentBike(_vehicle).Rotation;
            RentBikesDictionary.GetRentBike(_vehicle).Client = null;
        }
    }
}
