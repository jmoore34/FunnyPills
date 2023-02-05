using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using PlayerRoles;
using System.Collections.Generic;

namespace FunnyPills.Items
{
    [CustomItem(ItemType.SCP500)]
    internal class DisguisePills : CustomPill
    {
        public const int ItemId = 5003;
        public override uint Id { get; set; } = ItemId;
        public override char Letter { get; set; } = 'D';

        public override string Name { get; set; } = "<color=#4751f3>SCP-500-D</color>";
        public override string Description { get; set; } = "Disguise yourself as an allied class";
        public override float Weight { get; set; } = 0;
        public override SpawnProperties SpawnProperties { get; set; }
        private IDictionary<Player, RoleTypeId> playerDisguises { get; set; } = new Dictionary<Player, RoleTypeId>();

        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStart;
            Exiled.Events.Handlers.Player.UsingItem += OnUsingItem;
            Exiled.Events.Handlers.Player.UsedItem += OnUsedItem;
            Exiled.Events.Handlers.Player.Verified += OnPlayerVerified;
            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Server.RoundStarted -= OnRoundStart;
            Exiled.Events.Handlers.Player.UsingItem -= OnUsingItem;
            Exiled.Events.Handlers.Player.UsedItem -= OnUsedItem;
            Exiled.Events.Handlers.Player.Verified -= OnPlayerVerified;


            base.UnsubscribeEvents();
        }

        private void OnRoundStart()
        {
            playerDisguises.Clear();
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

        // when new players join, send them the disguise packet
        // so they see the fake appearance
        private void OnPlayerVerified(VerifiedEventArgs ev)
        {
            // Wait a bit to let the non-fake packet go first
            Timing.CallDelayed(1, () =>
            {
                foreach (KeyValuePair<Player, RoleTypeId> playerDisguise in playerDisguises)
                {
                    Player disguisedPlayer = playerDisguise.Key;
                    RoleTypeId disguise = playerDisguise.Value;
                    ev.Player.Connection.Send(
                        new RoleSyncInfo(disguisedPlayer.ReferenceHub, disguise, ev.Player.ReferenceHub));

                }
            });
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

                RoleTypeId disguise;
                switch (ev.Player.Role.Type)
                {
                    case RoleTypeId.Scientist:
                        disguise = RoleTypeId.NtfSergeant;
                        break;
                    case RoleTypeId.FacilityGuard:
                    case RoleTypeId.NtfPrivate:
                    case RoleTypeId.NtfSergeant:
                    case RoleTypeId.NtfSpecialist:
                    case RoleTypeId.NtfCaptain:
                        disguise = RoleTypeId.Scientist;
                        break;
                    case RoleTypeId.ClassD:
                        disguise = RoleTypeId.ChaosRifleman;
                        break;
                    case RoleTypeId.ChaosConscript:
                    case RoleTypeId.ChaosRifleman:
                    case RoleTypeId.ChaosRepressor:
                    case RoleTypeId.ChaosMarauder:
                        disguise = RoleTypeId.ClassD;
                        break;
                    default:
                        disguise = new List<RoleTypeId> {
                            RoleTypeId.Scp049,
                            RoleTypeId.Scp106,
                            RoleTypeId.Scp096,
                            RoleTypeId.Scp0492,
                            RoleTypeId.Scp939,
                            RoleTypeId.Scp173
                        }.RandomElement();
                        break;
                }
                playerDisguises[ev.Player] = disguise;
                ev.Player.ChangeAppearance(disguise);
                ev.Player.Broadcast(10, $"You now look like a <color={disguise.GetColor().ToHex()}>{disguise.GetFullName()}</color>!", Broadcast.BroadcastFlags.Normal, true);
            }
        }
    }
}
