using System;
using System.Linq;
using System.Text.RegularExpressions;
using CommandSystem;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.CustomItems.API.Features;
using PlayerRoles;
using PluginAPI.Core;
using PluginAPI.Core.Items;
using RemoteAdmin;
using UnityEngine;

namespace SCPReplacer
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class LocalCoordinates : ICommand
    {
        public string Command => "localcoordinates";
        public string[] Aliases => new[] { "lc" };
        public string Description => "Returns the coordinates you are looking at relative to the room";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var player = Exiled.API.Features.Player.Get(sender);
            var room = player.CurrentRoom;
            Vector3 forward = player.CameraTransform.forward;
            // Reference used: MapEditorReborn
            // https://github.com/Michal78900/MapEditorReborn/blob/059475cd822bccfa07a8a28d458516b720f2eeb2/MapEditorReborn/Commands/ToolgunCommands/CreateObject.cs#L56
            if (!Physics.Raycast(player.CameraTransform.position + forward, forward, out RaycastHit hit, 100f))
            {
                response = "Failed to raycast";
                return false;
            }
            var point = hit.point;
            response = $"Absolute position: {point}\n" +
                       $"Relative position: {room.Transform.InverseTransformPoint(point)}";
            return true;
        }
    }


    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class SpawnPills : ICommand
    {
        public string Command => "spawnpills";

        public string[] Aliases => new[] { "s" };

        public string Description => "Spawn SCP-500 at coordinates relative to the room spawn point";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count != 3)
            {
                response = "Need 3 arguments (x, y, & z coords)";
                return false;
            }

            Vector3 offset;
            try
            {
                offset = new Vector3(float.Parse(arguments.First()),
                    float.Parse(arguments.Skip(1).First()),
                    float.Parse(arguments.Skip(2).First()));
            } catch
            {
                response = "Coordinates must be numbers";
                return false;
            }

            var room = Exiled.API.Features.Player.Get(sender).CurrentRoom;

            var location = room.Transform.TransformPoint(offset);

            if (!CustomItem.TryGet("SCP-330-25", out CustomItem item)) {
                response = "Unable to get item with matching name";
                return false;
            }

            item.Spawn(location);
            response = "Spawned SCP-500";
            return true;
        }
    }
}