using Exiled.API.Enums;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using System.Linq;

namespace FunnyPills.Items
{
    [CustomItem(ItemType.SCP500)]
    internal class RaycastPills : CustomPill
    {
        public const int ItemId = 4998;
        public override uint Id { get; set; } = ItemId;
        public override char Letter { get; set; } = 'R';

        public override string Name { get; set; } = "<color=#9c0000>SCP-500-R</color>";
        public override string Description { get; set; } = "An anomolous instance of SCP-500 that cannot be picked up";
        public override float Weight { get; set; } = 0;
        public override SpawnProperties SpawnProperties { get; set; }

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

        protected override void OnPickingUp(PickingUpItemEventArgs ev)
        {
            ev.Player.ShowHint("<color=#9c0000>SCP 500-R cannot be picked up</color>");
            ev.IsAllowed = false;
            base.OnPickingUp(ev);
        }

        // for consistency with other pills, these also cannot be used in the pocket dimension
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
            }
        }
    }
}
