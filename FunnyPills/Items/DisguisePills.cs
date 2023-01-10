using Exiled.API.Extensions;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnyPills.Items
{
    [CustomItem(ItemType.SCP500)]
    internal class DisguisePills : CustomItem
    {
        public const int ItemId = 5003;
        public override uint Id { get; set; } = ItemId;
        public override string Name { get; set; } = "<color=#80ec7d>SCP-500-D</color>";
        public override string Description { get; set; } = "Disguise yourself as an allied class";
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
                RoleTypeId disguise;
                switch (ev.Player.Role.Type)
                {
                    case RoleTypeId.Scientist:
                        disguise = RoleTypeId.NtfSergeant;
                        break;
                    case RoleTypeId.FacilityGuard:
                    case RoleTypeId.NtfPrivate:
                    case RoleTypeId.NtfSergeant:
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
                            RoleTypeId.Scp939
                        }.RandomElement();
                        break;
                }
                ev.Player.ChangeAppearance(disguise);
                ev.Player.Broadcast(10, $"You now look like a <color={disguise.GetColor()}>{disguise.GetFullName()}</color>!");
            }
        }
    }
}
