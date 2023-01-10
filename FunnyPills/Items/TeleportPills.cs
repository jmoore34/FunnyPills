using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using PluginAPI.Core.Zones;
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
            Exiled.Events.Handlers.Player.UsedItem += OnUsedItem;
            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.UsedItem -= OnUsedItem;
            base.UnsubscribeEvents();
        }

        private void OnUsedItem(UsedItemEventArgs ev)
        {
            if (Check(ev.Item))
            {
                ev.Player.Broadcast((ushort)secondsBeforeTeleport, "You start to feel dizzy");
                Timing.CallDelayed(secondsBeforeTeleport, () =>
                {
                    var chosenRoom = PluginAPI.Core.Map.Rooms.Where(room =>
                        // don't tp to light unless not yet decontaminated
                        (room.Zone != MapGeneration.FacilityZone.LightContainment || !LightZone.IsDecontaminated)
                        // don't tp to heavy unless nuke hasn't gone off
                        && (room.Zone != MapGeneration.FacilityZone.HeavyContainment || !PluginAPI.Core.Warhead.IsDetonated)
                    ).RandomElement();
                    ev.Player.Teleport(chosenRoom);
                });
            }
        }
    }
}
