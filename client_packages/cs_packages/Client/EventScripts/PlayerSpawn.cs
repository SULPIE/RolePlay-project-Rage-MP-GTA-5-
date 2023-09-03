using RAGE;

namespace Client.EventScripts
{
    internal class PlayerSpawn : Events.Script
    {
        public PlayerSpawn() 
        {
            Events.OnPlayerSpawn += OnPlayerSpawn;
        }

        private void OnPlayerSpawn(Events.CancelEventArgs cancel)
        {
            RAGE.Game.Interior.EnableInteriorProp(246785, "interior_basic");
            RAGE.Game.Interior.EnableInteriorProp(246785, "chair01");
            RAGE.Game.Interior.EnableInteriorProp(246785, "equipment_basic");
            RAGE.Game.Interior.EnableInteriorProp(246785, "set_up");
            RAGE.Game.Interior.EnableInteriorProp(246785, "clutter");
            RAGE.Game.Interior.EnableInteriorProp(246785, "production");
            RAGE.Game.Interior.EnableInteriorProp(246785, "chair02");
            RAGE.Game.Interior.EnableInteriorProp(246785, "chair05");
            RAGE.Game.Interior.EnableInteriorProp(246785, "chair03");
            RAGE.Game.Interior.EnableInteriorProp(246785, "chair04");
            RAGE.Game.Interior.EnableInteriorProp(246785, "chair06");
            RAGE.Game.Interior.EnableInteriorProp(246785, "chair07");
            //RAGE.Game.Streaming.RequestAnimDict()
        }
    }
}
