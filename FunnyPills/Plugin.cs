using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using UnityEngine;

namespace SCPReplacer
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "Funny Pills";
        public override string Author => "Jonathan Moore";
        public override Version Version => new Version(1, 0, 0);

        // Singleton pattern allows easy access to the central state from other classes
        // (e.g. commands)
        public static Plugin Singleton { get; private set; }



        public override void OnEnabled()
        {
            // Set up the Singleton so we can easily get the instance with all the state
            // from another class.
            Singleton = this;
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStart;
            CustomItem.RegisterItems();
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            // Deregister event handlers
            Exiled.Events.Handlers.Server.RoundStarted -= OnRoundStart;

            // This will prevent commands and other classes from being able to access
            // any state while the plugin is disabled
            Singleton = null;
            CustomItem.UnregisterItems();
            base.OnDisabled();
        }

        // These event handlers can be pulled out to their own class if needed.
        // However, due to the small size of the plugin, I kept them in this class
        // to cut back on coupling. (Partial classes would be another alternative)
        public void OnRoundStart()
        {
            SpawnPills(new Vector3(1f, 1f, -7f));
            SpawnPills(new Vector3(1f, 1.5f, -7f));
            SpawnPills(new Vector3(1f, 2f, -7f));
            SpawnPills(new Vector3(0f, 1f, -7f));
            SpawnPills(new Vector3(0f, 1.5f, -7f));
            SpawnPills(new Vector3(0f, 2f, -7f));
            SpawnPills(new Vector3(-0.8f, 1f, -7f));
            SpawnPills(new Vector3(-0.8f, 1.5f, -7f));
            SpawnPills(new Vector3(-0.8f, 2f, -7f));
            SpawnPills(new Vector3(-2f, 0.1f, -7.3f));
            SpawnPills(new Vector3(-2f, 0.1f, 7.3f));
        }


        private void SpawnPills(Vector3 relative)
        {
            CustomItem.Get("SCP-330-25").Spawn(Room.Get(Exiled.API.Enums.RoomType.Lcz914).Transform.TransformPoint(relative));
        }
    }
}