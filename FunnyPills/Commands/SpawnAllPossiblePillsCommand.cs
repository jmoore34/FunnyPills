using CommandSystem;
using FunnyPills.SpawnRooms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnyPills.Commands
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
}
