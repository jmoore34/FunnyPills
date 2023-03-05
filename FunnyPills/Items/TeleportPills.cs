using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using Interactables.Interobjects.DoorUtils;
using MapGeneration;
using MEC;
using System;
using System.Linq;

namespace FunnyPills.Items
{
    [CustomItem(ItemType.SCP500)]
    internal class TeleportPills : CustomPill
    {
        public const int ItemId = 5001;
        public override uint Id { get; set; } = ItemId;
        public override char Letter { get; set; } = 'T';
        public override string Name { get; set; } = "<color=#73f1c9>SCP-500-T</color>";
        public override string Description { get; set; } = "Teleports you to a random location";
        public override float Weight { get; set; } = 0;
        public override SpawnProperties SpawnProperties { get; set; }
        private int secondsBeforeTeleport = 3;

        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.UsingItem += OnUsingItem;
            Exiled.Events.Handlers.Player.UsedItem += OnUsedItem;
            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.UsingItem -= OnUsingItem;
            Exiled.Events.Handlers.Player.UsedItem -= OnUsedItem;
            base.UnsubscribeEvents();
        }

        private void OnUsingItem(UsingItemEventArgs ev)
        {
            // work only on these custom pills
            if (!Check(ev.Item))
                return;

            // don't allow within pocket dimension
            if (ev.Player.CurrentRoom.Type == RoomType.Pocket)
            {
                ev.Player.ShowHint(PocketDimensionMessage, PocketDimensionMessageDuration);
                ev.IsAllowed = false;
                return;
            }
        }

        private void OnUsedItem(UsedItemEventArgs ev)
        {
            if (Check(ev.Item))
            {
                // Disallow special pill effects in pocket dimension
                if (ev.Player.CurrentRoom.Type == RoomType.Pocket)
                {
                    ev.Player.Broadcast(PocketDimensionMessageDuration, PocketDimensionMessage);
                    return;
                }

                ev.Player.Broadcast((ushort)secondsBeforeTeleport, "<color=#73f1c9>You start to feel dizzy</color>");
                Timing.CallDelayed(secondsBeforeTeleport, () =>
                {
                    var chosenRoom = PluginAPI.Core.Map.Rooms.Where(room =>
                        // don't tp to light unless not yet decontaminated & not nuked
                        (room.Zone != FacilityZone.LightContainment || (!Map.IsLczDecontaminated && !PluginAPI.Core.Warhead.IsDetonated))
                        // tp to surface if and only if (iff) nuke has gone off
                        // i.e. tp to room (room in facility, not on surface) xor (warhead has detonated)
                        // so no tping to facility if warhead detonated (not both facility & detonated)
                        // and no tping to not surface if warhead not detonated (not neither facility or detonated)
                        && (room.Zone != FacilityZone.Surface ^ PluginAPI.Core.Warhead.IsDetonated)
                        // don't tp to invalid rooms
                        && room.isActiveAndEnabled
                        && room.gameObject != null
                        && room.Name != RoomName.Pocket
                        && room.Name != RoomName.EzCollapsedTunnel
                        && room.Name != RoomName.EzEvacShelter
                        // testroom/dog's old room will tp the player into the pit
                        && room.Name != RoomName.HczTestroom
                        && room.Name != RoomName.Lcz173
                        && room.Name != RoomName.HczTesla
                        // exploit if they tp inside 079 cr and kill 079
                        && room.Name != RoomName.Hcz079
                    ).RandomElement();
                    Log.Info($"{ev.Player.Nickname} used SCP-500-T. Chosen room: {chosenRoom.Name} ({chosenRoom.Zone}), decontaminated: {Map.IsLczDecontaminated}, nuked: {PluginAPI.Core.Warhead.IsDetonated}");

                    // Fully load the room so that items spawn
                    try
                    {
                        var room = Room.Get(chosenRoom);
                        foreach (Door door in room.Doors)
                        {
                            // this doesn't actually open the door but rather just loads the room
                            DoorEvents.TriggerAction(door.Base, DoorAction.Opened, ev.Player.ReferenceHub);
                        }
                    }
                    catch (Exception e)
                    {
                        Log.Error($"Error while loading room for player {ev.Player.Nickname} using {Name}: {e}");
                    }

                    ev.Player.Teleport(chosenRoom);
                });
            }
        }
    }
}
