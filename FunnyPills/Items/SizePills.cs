using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using UnityEngine;

namespace FunnyPills.Items
{
    [CustomItem(ItemType.SCP500)]
    internal class SizePills : CustomPill
    {
        public const int ItemId = 5006;
        public override uint Id { get; set; } = ItemId;
        public override char Letter { get; set; } = 'M';

        public override string Name { get; set; } = "<color=#e370f3>SCP-500-M</color>";
        public override string Description { get; set; } = "Randomly change your model size";
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
                var sizes = new Vector3[]
                {
                    // inverted
                    new Vector3(1f, 1f, -1f),
                    // wide
                    new Vector3(1.7f, 0.2f, 1f),
                    // tall
                    new Vector3(1.2f, 1.1f, 1.8f),
                };
                ev.Player.Scale = sizes.RandomElement();
            }
        }
    }
}

