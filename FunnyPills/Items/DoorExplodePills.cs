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
    internal class DoorExplodePills : CustomItem
    {
        public const int ItemId = 5002;
        public override uint Id { get; set; } = ItemId;
        public override string Name { get; set; } = "SCP-500-X";
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
            if (ev.Item.Type == CustomItem.Get(Name).Type)
            {
                ev.Player.Broadcast(5, "Boom!");
            }
        }
    }
}
