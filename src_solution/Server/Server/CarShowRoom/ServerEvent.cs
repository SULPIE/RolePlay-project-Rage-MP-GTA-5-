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
            CarAutoShowRoom high = new CarAutoShowRoomHigh();
            eco.Init(eco.CarInfoArray, new Vector3(1120.1653f, -3195.5862f, -40.401608f), new Vector3(0f, 0f, -95.1163f), new Vector3(-185.0749f, -1318.8457f, 31.298126f), new Vector3(1131.1177f, -3197.344f, -39.936752f), new Vector3(-0.024868375f, 0.01584663f, -90.113686f), new Vector3(-179.90115f, -1320.3577f, 30.578419f), new Vector3(0f, 0f, -2.7096725), new Vector3(1118.773f, -3193.522f, -40.39267f));
            high.Init(high.CarInfoArray, new Vector3(-1357.6147f, 164.46469f, -99.182434f), new Vector3(0f, 0f, 177.6918f), new Vector3(120.040924f, -1113.526f, 29.22925f), new Vector3(-1351.5308f, 156.57584f, -99.46541f), new Vector3(0.0024073068f, 0.025250135f, 179.7439f), new Vector3(120.4108f, -1119.8059f, 28.662584f), new Vector3(0f, 0f, -6.8521547f), new Vector3(-1358.0675f, 167.55388f, -98.83674f));
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
            NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::DOWNLOAD_PROP_INTERIOR", carAutoShow.Id);

            player.SetData<CarAutoShowRoom>("car_show_room", carAutoShow);
            player.SetData<Vehicle>("show_room_vehicle", vehicle);
        }

        [ServerEvent(Event.PlayerEnterColshape)]
        public void OnPlayerExitColshape(ColShape colShape, Player player)
        {
            if (player.GetData<CarAutoShowRoom>("car_show_room") == null) { player.SendChatMessage("ХААААЙ"); return; }
            if(ShowRoomHolder.GetShowRoom(colShape) != null) { return; }
            if(player.GetData<CarAutoShowRoom>("car_show_room").ExitColshape != colShape) { return; }

            CarAutoShowRoom carAutoShow = player.GetData<CarAutoShowRoom>("car_show_room");
            Vehicle vehicle = player.GetData<Vehicle>("show_room_vehicle");
            player.Dimension = 0;
            player.Position = carAutoShow.ExitPosition;
            player.Rotation = carAutoShow.ExitRotation;
            vehicle.Delete();
            NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::PLAYER_BOUGHT_CAR");
            NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::UNLOAD_PROP_INTERIOR", carAutoShow.Id);
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

            player.SetData<Vehicle>("show_room_vehicle", vehicle);
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
            //NAPI.Resource.

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
                    NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::PLAYER_HAS_THE_AUTO");
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

        [ServerEvent(Event.PlayerEnterVehicle)]
        public void OnPlayerEnterShowRoomVehicle(Player player, Vehicle vehicle, sbyte seat)
        {
            if(vehicle == player.GetData<Vehicle>("show_room_vehicle"))
            {
                NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::PLAYER_VEHICLE_FREEZE");
            }
        }
    }
}
