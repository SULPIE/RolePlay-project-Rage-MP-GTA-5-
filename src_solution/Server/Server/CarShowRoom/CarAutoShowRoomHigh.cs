using GTANetworkAPI;
using Server.car_showroom;

namespace Server.CarShowRoom
{
    public class CarAutoShowRoomHigh: CarAutoShowRoom
    {
        public CarAutoShowRoomHigh()
        {
            _shorroomid = 1;

            _cars_info = new string[,]
            {
                {"baller", "70000"},
                {"baller2", "100000"},
                {"baller3", "110000"},
                {"baller4", "120000"},
                {"cavalcade", "71000"},
                {"cavalcade2", "77000"},
                {"dubsta", "120000"},
                {"dubsta2", "130000"},
                {"fq2", "140000"},
                {"rebla", "180000"},
                {"rocoto", "230000"},
                {"toros", "300000"},
                {"cognoscenti", "140000"},
                {"superd", "400000"},
                {"bestiagts", "500000"},
                {"comet2", "340000"},
                {"comet5", "390000"},
                {"coquette", "400000"},
                {"coquette4", "550000"},
                {"deveste", "600000"},
                {"komoda", "350000"},
                {"imorgon", "340000"},
                {"italigto", "440000"},
                {"jugular", "390000"},
                {"jester", "410000"},
                {"khamelion", "370000"},
                {"locust", "450000"},
                {"neo", "500000"},
                {"paragon", "490000"},
                {"panthere", "430000"},
                {"pariah", "450000"},
                {"raiden", "350000"},
                {"schlagen", "490000"},
                {"seven70", "500000"},
                {"vstr", "370000"},
                {"cypher", "300000"},
                {"cheetah2", "600000"},
                {"infernus2", "600000"},
                {"turismo2", "600000" },
                {"adder", "400000"},
                {"nero", "550000"},
                {"reaper", "600000"},
                {"sc1", "600000"},
                {"tempesta", "590000"},
                {"vacca", "570000"}
            };

            NAPI.Util.ConsoleOutput("Элитный автосалон загружен!");
        }
    }
}
