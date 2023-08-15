using GTANetworkAPI;
using MySql.Data.MySqlClient;
using Server.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Server.Houses
{
    public class HouseCommand : Script
    {
        [Command("createhouse")]
        public async Task CommandCreateHouse(Player player, int type, int cost)
        {
            if(type < 0 || type > (HouseTypesInfo.NameOfTypes.Length - 1))
            {
                player.SendChatMessage("Нет такого дома!");
                return;
            }

            if(cost < 1) { return; } 

            MySqlCommand command = new MySqlCommand
            (
                "INSERT INTO `houses` (`type`, `disc`, `cost`, `posx`, `posy`, `posz`, `dimension`) VALUES " +
                "(@type, @disc, @cost, @posx, @posy, @posz, @dimension)"
            );

            command.Parameters.AddWithValue("@type", type);
            command.Parameters.AddWithValue("@disc", HouseTypesInfo.NameOfTypes[type]);
            command.Parameters.AddWithValue("@cost", cost);
            command.Parameters.AddWithValue("@posx", player.Position.X);
            command.Parameters.AddWithValue("@posy", player.Position.Y);
            command.Parameters.AddWithValue("@posz", player.Position.Z);
            command.Parameters.AddWithValue("@dimension", player.Dimension);

            await Query.Execute(command);

            int id = (int)command.LastInsertedId;

            House house = new House();

            NAPI.Task.Run(() =>
            {
                house.Init
                (
                    id,
                    type,
                    player.Position,
                    HouseTypesInfo.HouseTypesInfoArray[type, (int)HouseTypesInfo.HouseInteriorPositions.PlayerPosition],
                    HouseTypesInfo.HouseTypesInfoArray[type, (int)HouseTypesInfo.HouseInteriorPositions.ExitPickup],
                    (int)player.Dimension,
                    cost,
                    "null"
                );
            });
        }
    }
}
