using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs;
using Exiled.Events.EventArgs.Player;
using FunnyPills;
using FunnyPills.Items;
using FunnyPills.SpawnRooms;
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
            SpawnRoomUtils.SpawnPills();
        }
    }
}