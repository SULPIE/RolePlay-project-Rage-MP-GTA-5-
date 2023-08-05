using GTANetworkAPI;
using Server.car_showroom;
using System.Text.Json;

namespace Server.CarShowRoom
{
    public class ServerEvent : Script
    {
        [ServerEvent(Event.ResourceStart)]
        public void OnResourceStart()
        {
            CarAutoShowRoom eco = new CarAutoShowRoomEco();
            eco.Init(eco.CarInfoArray, new Vector3(170.89429f, -1006.1687f, -98.999886f), new Vector3(0f, 0f, -3.1852312f), new Vector3(-185.0749f, -1318.8457f, 31.298126f), new Vector3(173.33844f, -1002.4945f, -99.50787f), new Vector3(0f, 0f, 1.6677272f));
        }

        [ServerEvent(Event.PlayerEnterColshape)]
        public void OnPlayerEnterColshape(ColShape colShape, Player player) 
        {
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

        [RemoteEvent("CLIENT:SERVER::ON_BUTTON_CHANGE_CAR_CLICK")]
        public void OnButtonNextClick(Player player, int current_car)
        {
            Vehicle vehicle = player.GetData<Vehicle>("show_room_vehicle");
            vehicle.Delete();
            CarAutoShowRoom carAutoShow = player.GetData<CarAutoShowRoom>("car_show_room");

            uint hash = NAPI.Util.GetHashKey(carAutoShow.CarInfoArray[current_car, 0]);
            vehicle = NAPI.Vehicle.CreateVehicle(hash, carAutoShow.CarPosition, carAutoShow.CarRotation.Z, 0, 0, dimension: player.Id, engine: false);

            NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::REFRESH_UI", carAutoShow.CarInfoArray[current_car, 0], carAutoShow.CarInfoArray[current_car, 1]);
        }

        [RemoteEvent("CLIENT:SERVER::ON_CHANGE_COLOR")]
        public void OnChangeColor(Player player, int color)
        {
            Vehicle vehicle = player.GetData<Vehicle>("show_room_vehicle");
            vehicle.PrimaryColor = color;
            vehicle.SecondaryColor = color;
        }
    }
}
