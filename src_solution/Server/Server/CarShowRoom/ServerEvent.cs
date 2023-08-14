using Google.Protobuf.WellKnownTypes;
using GTANetworkAPI;
using Server.AccountInfo;
using Server.car_showroom;
using Server.Vehicles.PersonalVehicles;
using System;

namespace Server.CarShowRoom
{
    public class ServerEvent : Script
    {
        [ServerEvent(Event.ResourceStart)]
        public void OnResourceStart()
        {
            CarAutoShowRoom eco = new CarAutoShowRoomEco();
            eco.Init(eco.CarInfoArray, new Vector3(170.89429f, -1006.1687f, -98.999886f), new Vector3(0f, 0f, -3.1852312f), new Vector3(-185.0749f, -1318.8457f, 31.298126f), new Vector3(173.33844f, -1002.4945f, -99.50787f), new Vector3(0f, 0f, 1.6677272f), new Vector3(-179.90115f, -1320.3577f, 30.578419f), new Vector3(0f, 0f, -2.7096725), new Vector3(172.0536f, -1007.8245f, -98.99989f));
        }

        [ServerEvent(Event.PlayerEnterColshape)]
        public void OnPlayerEnterColshape(ColShape colShape, Player player) 
        {
            if(ShowRoomHolder.GetShowRoom(colShape) == null) { return; }
            if(ShowRoomHolder.GetShowRoom(colShape).EnterColshape != colShape) { return; }

            CarAutoShowRoom carAutoShow = ShowRoomHolder.GetShowRoom(colShape);

            player.Position = carAutoShow.PlayerPosition;
            player.Rotation = carAutoShow.PlayerRotation;
            player.Dimension = player.Id;

            uint hash = NAPI.Util.GetHashKey(carAutoShow.CarInfoArray[0, 0]);
            Vehicle vehicle = NAPI.Vehicle.CreateVehicle(hash, carAutoShow.CarPosition, carAutoShow.CarRotation.Z, 0, 0, dimension: player.Id, engine: false);

            NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::DRAW_SHOWROOM_UI", carAutoShow.CarInfoArray[0,0], carAutoShow.CarInfoArray[0, 1], carAutoShow.CarInfoArray.GetLength(0));

            player.SetData<CarAutoShowRoom>("car_show_room", carAutoShow);
            player.SetData<Vehicle>("show_room_vehicle", vehicle);
        }

        [ServerEvent(Event.PlayerEnterColshape)]
        public void OnPlayerExitColshape(ColShape colShape, Player player)
        {
            if (player.GetData<CarAutoShowRoom>("car_show_room") == null) { return; }
            if (ShowRoomHolder.GetShowRoom(colShape).ExitColshape != colShape) { return; }

            CarAutoShowRoom carAutoShow = player.GetData<CarAutoShowRoom>("car_show_room");
            Vehicle vehicle = player.GetData<Vehicle>("show_room_vehicle");
            player.Dimension = 0;
            player.Position = carAutoShow.ExitPosition;
            player.Rotation = carAutoShow.ExitRotation;
            vehicle.Delete();
            NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::PLAYER_BOUGHT_CAR");
        }

            [RemoteEvent("CLIENT:SERVER::ON_BUTTON_CHANGE_CAR_CLICK")]
        public void OnButtonNextClick(Player player, int current_car)
        {
            Vehicle vehicle = player.GetData<Vehicle>("show_room_vehicle");
            vehicle.Delete();
            CarAutoShowRoom carAutoShow = player.GetData<CarAutoShowRoom>("car_show_room");

            uint hash = NAPI.Util.GetHashKey(carAutoShow.CarInfoArray[current_car, 0]);
            vehicle = NAPI.Vehicle.CreateVehicle(hash, carAutoShow.CarPosition, carAutoShow.CarRotation.Z, 0, 0, dimension: player.Id, engine: false);
            vehicle.Dimension = player.Dimension;

            NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::REFRESH_UI", carAutoShow.CarInfoArray[current_car, 0], carAutoShow.CarInfoArray[current_car, 1]);
        }

        [RemoteEvent("CLIENT:SERVER::ON_CHANGE_COLOR")]
        public void OnChangeColor(Player player, int color)
        {
            Vehicle vehicle = player.GetData<Vehicle>("show_room_vehicle");
            vehicle.PrimaryColor = color;
            vehicle.SecondaryColor = color;
        }

        [RemoteEvent("CLIENT:SERVER::CLICK_BUTTON_CONFIRM")]
        public void OnButtonClickConfirm(Player player, int current_car_id, int current_color_id, int max_color)
        {
            if(AccountHandlerDictionary.GetAccount(player) == null) { return; }

            Vehicle vehicle = player.GetData<Vehicle>("show_room_vehicle");
            CarAutoShowRoom carAutoShow = player.GetData<CarAutoShowRoom>("car_show_room");

            if (current_car_id > carAutoShow.CarInfoArray.GetLength(0)) { return; }
            if (current_color_id > max_color) { return; }

            if (AccountHandlerDictionary.GetAccount(player).Money < Convert.ToInt32(carAutoShow.CarInfoArray[current_car_id, 1]))
            {
                NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::NOT_ENOUGH_MONEY_HANDLER");
                player.SendChatMessage("~r~[Ошибка]~w~:Недостаточно средств");
                return;
            }

            foreach(Vehicle veh in NAPI.Pools.GetAllVehicles())
            {
                if(PersonalVehicleHolder.GetPersonalVehicle(veh) == null) {  continue; }

                if(PersonalVehicleHolder.GetPersonalVehicle(veh).Owner == player.Name)
                {
                    player.SendChatMessage("~r~[Ошибка]~w~:У вас уже есть автомобиль");
                    return;
                }
            }

            PersonalVehicle personalVehicle = new PersonalVehicle(carAutoShow.CarInfoArray[current_car_id, 0], player.Name, 1000, "", current_color_id, current_color_id, carAutoShow.ExitPosition, carAutoShow.ExitRotation);
            InsertVehicle.Start(player, carAutoShow.CarInfoArray[current_car_id, 0], current_color_id, current_color_id, "", carAutoShow.ExitPosition, carAutoShow.ExitRotation);

            player.Dimension = 0;
            player.Position = carAutoShow.ExitPosition;
            player.Rotation = carAutoShow.ExitRotation;
            player.SetIntoVehicle(personalVehicle.Vehicle, -1);

            vehicle.Delete();

            NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::PLAYER_BOUGHT_CAR");
        }
    }
}
