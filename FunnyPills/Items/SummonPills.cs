using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using PlayerRoles;
using System.Linq;

namespace FunnyPills.Items
{
    [CustomItem(ItemType.SCP500)]
    internal class SummonPills : CustomPill
    {
        public const int ItemId = 5004;
        public override uint Id { get; set; } = ItemId;
        public override char Letter { get; set; } = 'A';
        public override string Name { get; set; } = "<color=#60f04c>SCP-500-A</color>";
        public override string Description { get; set; } = "Summons an ally from the dead";
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

        private void OnUsingItem(UsingItemEventArgs ev)
        {
            // work only on these custom pills
            if (!Check(ev.Item))
                return;

            // These pills need a spectator to be able to work

            // If no spectators:
            if (Player.List.Where(p => p.Role.Type == RoleTypeId.Spectator).Count() <= 0)
            {
                ev.Player.Broadcast(5, "<color=#ed98a2>There are not enough spectators yet to use SCP-500-A.</color>");
                ev.IsAllowed = false;
            }
        }

        private void OnUsedItem(UsedItemEventArgs ev)
        {
            if (!Check(ev.Item))
                return;

            var spawnPlayer = Util.GetRandomSpectatorOrNull();
            if (spawnPlayer == null)
            {
                ev.Player.Broadcast(6, "<color=#d53f51>But nobody came...</color>");
            }
            else
            {
                RoleTypeId spawnPlayerRole;
                switch (ev.Player.Role.Type)
                {
                    case RoleTypeId.ClassD:
                    case RoleTypeId.ChaosConscript:
                    case RoleTypeId.ChaosRifleman:
                    case RoleTypeId.ChaosRepressor:
                    case RoleTypeId.ChaosMarauder:
                        spawnPlayerRole = RoleTypeId.ChaosConscript;
                        break;
                    case RoleTypeId.Scientist:
                    case RoleTypeId.FacilityGuard:
                    case RoleTypeId.NtfPrivate:
                    case RoleTypeId.NtfSergeant:
                    case RoleTypeId.NtfSpecialist:
                    case RoleTypeId.NtfCaptain:
                        spawnPlayerRole = RoleTypeId.FacilityGuard;
                        break;
                    default:
                        spawnPlayerRole = RoleTypeId.Tutorial;
                        break;
                }
                // todo: add serpents custom class if tutorial
                ev.Player.Broadcast(5, $"Summoning a <color={spawnPlayerRole.GetColor().ToHex()}>{spawnPlayerRole.GetFullName()}</color>");
                spawnPlayer.Role.Set(spawnPlayerRole, Exiled.API.Enums.SpawnReason.Respawn, RoleSpawnFlags.All);
                spawnPlayer.Broadcast(7, "<color=#fffea7>You have been summoned by</color> <color=#83f546>SCP-500-A</color>");
                // wait a bit to allow the spawn to finish
                Timing.CallDelayed(1, () =>
                {
                    spawnPlayer.Teleport(ev.Player);
                });
            }
        }
    }
}
