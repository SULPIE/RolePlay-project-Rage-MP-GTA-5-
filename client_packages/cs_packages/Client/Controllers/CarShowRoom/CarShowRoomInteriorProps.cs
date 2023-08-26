using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Controllers.CarShowRoom
{
    public static class CarShowRoomInteriorProps
    {
        private static string[,] _props_arr =
        {
            {"","","","","","","","","","","","","","","","","","","","","","",""},
            {"entity_set_bedroom", "entity_set_bedroom_empty", "entity_set_bombs", "entity_set_cabinets", "entity_set_car_lift_default", "entity_set_chalkboard", "entity_set_container", "entity_set_cut_seats", "entity_set_def_table", "entity_set_ecu", "entity_set_IAA", "entity_set_jammers", "entity_set_laptop", "entity_set_lightbox", "entity_set_methLab", "entity_set_plate", "entity_set_scope", "entity_set_style_1", "entity_set_table", "entity_set_thermal", "entity_set_tints", "entity_set_train", "entity_set_virus"}
        };
        private static int[] _interior_ids =
        {
            0,
            285953
        };

        public static void DownLoadProps(int show_room_id)
        {
            for(int i = 0; i < _props_arr.GetLength(0); i++)
            {
                if (_props_arr[show_room_id, i] != "" && _interior_ids[show_room_id] != 0)
                {
                    RAGE.Game.Interior.EnableInteriorProp(_interior_ids[show_room_id], _props_arr[show_room_id, i]);
                }
            }
        }

        public static void UnloadProps(int show_room_id)
        {
            for (int i = 0; i < _props_arr.GetLength(0); i++)
            {
                if (_props_arr[show_room_id, i] != "" && _interior_ids[show_room_id] != 0)
                {
                    RAGE.Game.Interior.DisableInteriorProp(_interior_ids[show_room_id], _props_arr[show_room_id, i]);
                }
            }
        }
    }
}
