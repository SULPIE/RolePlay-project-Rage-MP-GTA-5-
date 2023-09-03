using GTANetworkAPI;

namespace Server.AutoSchoolSys
{
    public static class Util
    {
        public static readonly Vector3 EnterPickupPosition = new Vector3(109.847176f, -1089.3301f, 29.302471f);
        public static readonly Vector3 ExitPickupPosition = new Vector3(1173.6167f, -3196.5176f, -39.007946f);
        public static readonly Vector3 InteriorCord = new Vector3(1171.7395f, -3195.0957f, -39.007946f);
        public static readonly Vector3 InteriorRot = new Vector3(0f, 0f, 86.539955f);

        public static readonly Vector3[] ExaminePickupsArray = {
            new Vector3(1162.207f, -3196.319f, -39.007988f),
            new Vector3(1169.4277f, -3198.4658f, -39.007988f),
            new Vector3(1160.2948f, -3192.2725f, -39.007988f),
            new Vector3(1162.6647f, -3196.3452f, -39.007946f),
            new Vector3(1167.004f, -3195.8599f, -39.007946f)
        };

        public static readonly Vector3[,] ExamineVehiclesCords = {
            { new Vector3(139.67781f, -1082.08f, 28.7964f), new Vector3(-0.020063918f, 0.0034489539f, -1.3180648f) },
            { new Vector3(143.60306f, -1081.0746f, 28.795557f), new Vector3(0.103264935f, 0.0051846965f, 0.8526568f) },
            { new Vector3(147.24878f, -1081.0308f, 28.794931f), new Vector3(0.014535377f, -0.0006954018, -0.64377755f) },
            { new Vector3(150.77217f, -1081.1154f, 28.800344f), new Vector3(0.2945289f, -0.55753696f, 2.0199327f) },
            { new Vector3(154.718f, -1081.1661f, 28.795116f), new Vector3(0.11107419f, -0.0182722f, 0.08989745f) },
            { new Vector3(158.4298f, -1081.2932f, 28.795572f), new Vector3(-0.029182648f, 0.024053551f, 0.35990945f) }
        };
    }
}
