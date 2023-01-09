using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnyPills.Items
{
    [CustomItem(ItemType.SCP500)]
    internal class TeleportPills : CustomItem
    {
        public const int ItemId = 5001;
        public override uint Id { get; set; } = ItemId;
        public override string Name { get; set; } = "SCP-500-T";
        public override string Description { get; set; } = "Teleports you to a random location";
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
                ev.Player.Broadcast(5, "You start to feel dizzy");
            }
        }
    }
}
