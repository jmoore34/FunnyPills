using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using Mirror;
using PlayerRoles;
using System.Collections.Generic;

namespace FunnyPills.Items
{
    [CustomItem(ItemType.SCP500)]
    internal class LookDifferentTeamPills : CustomPill
    {
        public const int ItemId = 5008;
        public override uint Id { get; set; } = ItemId;
        public override char Letter { get; set; } = 'L';

        public override string Name { get; set; } = "<color=#eaeb7f>SCP-500-{Letter}</color>";
        public override string Description { get; set; } = $"Look like a member of a different team for {durationSeconds} seconds";
        public override float Weight { get; set; } = 0;
        public override SpawnProperties SpawnProperties { get; set; }

        const int durationSeconds = 12;

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

                var scpDisguises = new RoleTypeId[] {
                    RoleTypeId.Scp049,
                    RoleTypeId.Scp0492,
                    RoleTypeId.Scp096,
                    RoleTypeId.Scp106,
                    RoleTypeId.Scp173,
                    RoleTypeId.Scp939,
                };

                var foundationDisguises = new RoleTypeId[]
                {
                    RoleTypeId.Scientist,
                    RoleTypeId.NtfCaptain,
                    RoleTypeId.NtfPrivate,
                    RoleTypeId.FacilityGuard
                };

                var chaosDisguises = new RoleTypeId[]
                {
                    RoleTypeId.ClassD,
                    RoleTypeId.ChaosConscript,
                    RoleTypeId.ChaosMarauder
                };

                RoleTypeId disguise;
                if (ev.Player.Role.Side == Side.Mtf)
                {
                    if (UnityEngine.Random.value < 0.4)
                    {
                        disguise = scpDisguises.RandomElement();
                    } else
                    {
                        disguise = chaosDisguises.RandomElement();
                    }
                } else if (ev.Player.Role.Side == Side.ChaosInsurgency)
                {
                    if (UnityEngine.Random.value < 0.35)
                    {
                        disguise = scpDisguises.RandomElement();
                    } else
                    {
                        disguise = foundationDisguises.RandomElement();
                    }
                }
                else
                {
                    // if player is serpents etc
                    if (UnityEngine.Random.value < 0.4)
                    {
                        disguise = chaosDisguises.RandomElement();
                    } else
                    {
                        disguise = foundationDisguises.RandomElement();
                    }
                }

                ev.Player.ChangeAppearance(disguise);
                ev.Player.Broadcast(10, $"You now look like a <color={disguise.GetColor().ToHex()}>{disguise.GetFullName()}</color>!", Broadcast.BroadcastFlags.Normal, true);

                Timing.CallDelayed(durationSeconds, () =>
                {
                    var trueForm = ev.Player.Role.Type;
                    ev.Player.ChangeAppearance(trueForm);
                    ev.Player.Broadcast(10, $"<color=#eaeb7f>Your appearance has reverted to its true form.</color>\nYou now look like a <color={trueForm.GetColor().ToHex()}>{trueForm.GetFullName()}</color>");
                });
            }
        }
    }
}
