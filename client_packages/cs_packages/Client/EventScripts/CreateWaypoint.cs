using RAGE;
using System;

namespace Client.EventScripts
{
    internal class CreateWaypoint : Events.Script
    {
        public CreateWaypoint()
        {
            Events.OnPlayerCreateWaypoint += OnPlayerCreateWaypoint;
        }

        private void OnPlayerCreateWaypoint(Vector3 position)
        {
            Events.CallRemote("CLIENT:SERVER::CREATE_WAYPOINT", position.X, position.Y, position.Z);
        }
    }
}
