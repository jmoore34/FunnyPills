using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Pickups;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using InventorySystem.Items.Firearms.Attachments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FunnyPills.Items
{
    [CustomItem(ItemType.GunE11SR)]
    internal class RaycastingGun : CustomWeapon
    {
        public const int ItemId = 4999;
        public override uint Id { get; set; } = ItemId;
        public override string Name { get; set; } = "RaycastingGun";
        public override string Description { get; set; } = "Spawn SCP-500 at the viewport point and log the room-local location";
        public override float Weight { get; set; }

        public override float Damage { get; set; } = 200;
        public override byte ClipSize { get; set; } = 41;

        public override SpawnProperties SpawnProperties { get; set; }
        public override AttachmentName[] Attachments { get; set; } = new AttachmentName[]
        {
            AttachmentName.ScopeSight,
            AttachmentName.Laser
        };

        private Pickup lastSpawnedItem = null;

        protected override void OnShooting(ShootingEventArgs ev)
        {
            var player = ev.Player;
            var room = player.CurrentRoom;
            var hintPrefix = "\n\n\n";
            Vector3 forward = player.CameraTransform.forward;
            // Reference used: MapEditorReborn
            // https://github.com/Michal78900/MapEditorReborn/blob/059475cd822bccfa07a8a28d458516b720f2eeb2/MapEditorReborn/Commands/ToolgunCommands/CreateObject.cs#L56
            if (!Physics.Raycast(player.CameraTransform.position + forward, forward, out RaycastHit hit, 100f))
            {
                player.ShowHint(hintPrefix + "<color=#ff4e4e>Raycast failure</color>");
                player.SendConsoleMessage("Raycast failure", "red");
                return;
            }
            var absolutePosition = hit.point;
            var localPosition = room.Transform.InverseTransformPoint(absolutePosition);
            var message = $"<color=#71eeff>{room.Name}</color> {localPosition}";
            player.ShowHint(message);
            player.SendConsoleMessage(message, "white");

            if (lastSpawnedItem != null)
            {
                lastSpawnedItem.UnSpawn();
                lastSpawnedItem = null;
            }

            var newItem = CustomItem.Get(TeleportPills.ItemId);
            lastSpawnedItem = newItem.Spawn(absolutePosition);
            ev.IsAllowed = false; // don't fire an actual shot
        }
    }
}
