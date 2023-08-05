using GTANetworkAPI;

namespace Server.car_showroom
{
    public class CarAutoShowRoomEco : CarAutoShowRoom
    {
        public CarAutoShowRoomEco()
        {
            _cars_info = new string[,]
            {
                {"club", "2000"},
                {"rhapsody", "2100"},
                {"brioso2", "1800"},
                {"weevil", "1700"},
                {"panto", "2500"},
                {"dilettante", "2100"},
                {"blade", "4400"},
                {"broadway", "2600"},
                {"faction", "3200"},
                {"eudora", "3300"},
                {"bodhi2", "4000"},
                {"rancherxl", "4200"},
                {"habanero", "4900"},
                {"asea", "4500"},
                {"emperor", "3000"},
                {"glendale2", "3400"},
                {"ingot", "3300"},
                {"primo2", "2300"},
                {"stanier", "2700"},
                {"washington", "3300"},
                {"manana2", "4000"},
                {"pigalle", "4500"},
                {"retinue", "4000"},
                {"savestra", "6400"},
                {"tornado", "4000"},
                {"cheburek", "9000"},
                {"clique2", "6000"},
                {"mesa", "5600"},
                {"serrano", "8000"},
                {"buffalo", "14000"},
                {"futo2", "17000"},
                {"fagaloa", "4500"},
                {"nebula", "5200"},
                {"dukes", "7300"},
                {"tulip2", "5300"},
                {"vamos", "5300"},
                {"virgo3", "6600"},
                {"voodoo", "5500"}
            };

            NAPI.Util.ConsoleOutput("Уличный автосалон загружен!");
        }
    }
}
