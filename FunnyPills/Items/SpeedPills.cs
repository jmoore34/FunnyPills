using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;

namespace FunnyPills.Items
{
    [CustomItem(ItemType.SCP500)]
    internal class SpeedPills : CustomPill
    {
        public const int ItemId = 5005;
        public override uint Id { get; set; } = ItemId;
        public override char Letter { get; set; } = 'S';

        public override string Name { get; set; } = "<color=#47e5f5>SCP-500-S</color>";
        public override string Description { get; set; } = "Randomly changes your speed";
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
                var duration = 8;

                if (UnityEngine.Random.value < 0.75)
                {
                    ev.Player.EnableEffect(Exiled.API.Enums.EffectType.MovementBoost, 8);
                    ev.Player.ChangeEffectIntensity(Exiled.API.Enums.EffectType.MovementBoost, 200);
                    ev.Player.Broadcast(8, "<color=#47e5f5>You feel energized</color>");
                }
                else
                {
                    ev.Player.EnableEffect(Exiled.API.Enums.EffectType.Disabled, 8);
                    ev.Player.EnableEffect(Exiled.API.Enums.EffectType.Concussed, 8);
                    ev.Player.Broadcast(8, "<color=#9c4646>You don't feel so good...</color>");
                }
                Timing.CallDelayed(8, () =>
                {
                    ev.Player.DisableEffect(Exiled.API.Enums.EffectType.MovementBoost);
                    ev.Player.DisableEffect(Exiled.API.Enums.EffectType.Disabled);
                    ev.Player.DisableEffect(Exiled.API.Enums.EffectType.Concussed);

                });
            }
        }

    }
}

