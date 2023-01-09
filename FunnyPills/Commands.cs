using System;
using System.Linq;
using System.Text.RegularExpressions;
using CommandSystem;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.CustomItems.API.Features;
using FunnyPills.Items;
using FunnyPills.SpawnRooms;
using InventorySystem;
using PlayerRoles;
using PluginAPI.Core;
using PluginAPI.Core.Items;
using RemoteAdmin;
using UnityEngine;

namespace SCPReplacer
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class SpawnAllPillsCommand : ICommand
    {
        public string Command => "spawnallpills";
        public string[] Aliases => new[] { "sap" };
        public string Description => "Spawn all possible funny pill locations in every spawn room";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission(PlayerPermissions.GivingItems))
            {
                response = "You need item spawning perms in order to use this command.";
                return false;
            }

            SpawnRoomUtils.SpawnEveryPossiblePill();

            response = "Spawned all possible funny pill locations in every spawn room";
            return true;
        }
    }

    [CommandHandler(typeof(ClientCommandHandler))]
    public class RaytracingGunCommand : ICommand
    {
        public string Command => "raytracinggun";
        public string[] Aliases => new[] { "rg" };
        public string Description => "Gives you a raytracing gun";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission(PlayerPermissions.GivingItems))
            {
                response = "You need item spawning perms in order to use this command.";
                return false;
            }

            var player = Exiled.API.Features.Player.Get(sender);
            CustomItem.Get("RaytracingGun").Give(player);
            response = "Gave a raytracing gun";
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

            if (!CustomItem.TryGet("SpawnPills", out CustomItem item)) {
                response = "Unable to get item with matching name";
                return false;
            }

            item.Spawn(location);
            response = "Spawned SCP-500";
            return true;
        }
    }
}