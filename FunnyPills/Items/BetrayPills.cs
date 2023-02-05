using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using InventorySystem;
using PlayerRoles;

namespace FunnyPills.Items
{
    [CustomItem(ItemType.SCP500)]
    internal class BetrayPills : CustomPill
    {
        public const int ItemId = 5007;
        public override uint Id { get; set; } = ItemId;
        public override char Letter { get; set; } = 'B';

        public override string Name { get; set; } = "<color=#c62424>SCP-500-B</color>";
        public override string Description { get; set; } = "Betray your team";
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

                RoleTypeId newRole;
                switch (ev.Player.Role.Type)
                {
                    // scientist <-> class d
                    case RoleTypeId.Scientist:
                        newRole = RoleTypeId.ClassD;
                        break;
                    case RoleTypeId.ClassD:
                        newRole = RoleTypeId.Scientist;
                        break;

                    // ntf -> chaos
                    case RoleTypeId.NtfPrivate:
                        newRole = RoleTypeId.ChaosConscript;
                        break;
                    case RoleTypeId.NtfSergeant:
                        newRole = RoleTypeId.ChaosMarauder;
                        break;
                    case RoleTypeId.NtfSpecialist:
                        newRole = RoleTypeId.ChaosRifleman;
                        break;
                    case RoleTypeId.NtfCaptain:
                        newRole = RoleTypeId.ChaosRepressor;
                        break;


                    // chaos -> ntf
                    case RoleTypeId.ChaosConscript:
                        newRole = RoleTypeId.NtfPrivate;
                        break;
                    case RoleTypeId.ChaosMarauder:
                        newRole = RoleTypeId.NtfSergeant;
                        break;
                    case RoleTypeId.ChaosRifleman:
                        newRole = RoleTypeId.NtfSpecialist;
                        break;
                    case RoleTypeId.ChaosRepressor:
                        newRole = RoleTypeId.NtfCaptain;
                        break;

                    // guard -> zombie
                    case RoleTypeId.FacilityGuard:
                        newRole = RoleTypeId.Scp0492;
                        break;

                    // tutorial/etc -> private
                    default:
                        newRole = RoleTypeId.NtfPrivate;
                        break;
                }
                ev.Player.Inventory.ServerDropEverything();
                ev.Player.Role.Set(newRole, Exiled.API.Enums.SpawnReason.ForceClass, RoleSpawnFlags.AssignInventory);
                ev.Player.Broadcast(10, $"You betrayed your faction and became a <color={newRole.GetColor().ToHex()}>{newRole.GetFullName()}</color>!\n<color=#ef9228>Your old items have dropped on the ground.</color>", Broadcast.BroadcastFlags.Normal, true);
            }
        }
    }
}
