using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;

namespace FunnyPills.Items
{
    [CustomItem(ItemType.SCP500)]
    internal class DoorExplodePills : CustomPill
    {
        public const int ItemId = 5002;
        public override uint Id { get; set; } = ItemId;
        public override char Letter { get; set; } = 'X';

        public override string Name { get; set; } = "<color=#f17d7d>SCP-500-X</color>";
        public override string Description { get; set; } = "Explodes nearby doors";
        public override float Weight { get; set; } = 0;
        public override SpawnProperties SpawnProperties { get; set; }

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
                foreach (var door in ev.Player.CurrentRoom.Doors)
                {
                    door.BreakDoor(); // if breakable
                    door.TryPryOpen(); // if pryable
                }
            }
        }
    }
}
