using GTANetworkAPI;
using Server.AccountInfo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Vehicles.PersonalVehicles
{
    public class CommandParkVehicle : Script
    {
        [Command("park")]
        public void ParkVehicle(Player player)
        {
            if(AccountHandlerDictionary.GetAccount(player) == null) { return; }
            Vehicle vehicle = NAPI.Player.GetPlayerVehicle(player);
            if(PersonalVehicleHolder.GetPersonalVehicle(vehicle) == null) { return; }
            if(PersonalVehicleHolder.GetPersonalVehicle(vehicle).Owner != player.Name) { return; }

            if(vehicle.Position.Equals(PersonalVehicleHolder.GetPersonalVehicle(vehicle).Position))
            {
                player.SendChatMessage("~r~[Ошибка]~w~:Автомобиль уже припаркован на этом месте.");
            }

            player.SendChatMessage("~g~Вы успешно припарковали свой автомобиль");
            PersonalVehicleHolder.GetPersonalVehicle(vehicle).Position = vehicle.Position;
            PersonalVehicleHolder.GetPersonalVehicle(vehicle).Rotation = vehicle.Rotation;

            NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::PLAY_AUDIO_CONFIRM_BEEP");

            List<Player> _nearby_players = NAPI.Player.GetPlayersInRadiusOfPlayer(10.0f, player);

            foreach (Player _player in _nearby_players)
            {
                if (_player.Dimension == player.Dimension)
                {
                    NAPI.ClientEvent.TriggerClientEvent(_player, "SERVER:CLIENT::PLAY_AUDIO_CONFIRM_BEEP");
                }
            }
        }
    }
}
