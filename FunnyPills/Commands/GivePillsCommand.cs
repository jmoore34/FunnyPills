using CommandSystem;
using Exiled.API.Features;
using Exiled.CustomItems.API.Features;
using FunnyPills.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FunnyPills.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class GivePillsCommand : ICommand
    {
        public string Command => "givepills";
        public string[] Aliases => new[] { "gp", "p", "scp500" };
        public string Description => "Gives you funny pills with corresponding letters. E.g., `givepills TXX <optional user id or name>` gives one SCP-500-T and two SCP-500-Xs.";

        private string UsageInfo => "<color=white>Usage: </color><color=#e2f080>givepills <required 1 or more pill letters> [optional player id/name]</color>\n"
            + "<color=white>Example: </color><color=#f9d767>givepills TTX</color><color=white> gives one SCP-500-T and two SCP-500-Xs</color>\n"
            + SpawnItemUtils.PillDescriptions;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission(PlayerPermissions.GivingItems))
            {
                response = "No Permission";
                return false;
            }

            if (arguments.Count <= 0)
            {
                response = UsageInfo;
                return false;
            }



            Player player;
            if (arguments.Count < 2)
            {
                player = Player.Get(sender);
            }
            else
            {
                var requestedPlayer = arguments.Skip(1).First();
                try
                {
                    player = Player.List.First(p =>
                        p.Id.ToString() == requestedPlayer
                        || p.Nickname.Equals(requestedPlayer, StringComparison.OrdinalIgnoreCase)
                        || p.DisplayNickname.Equals(requestedPlayer, StringComparison.OrdinalIgnoreCase));
                }
                catch
                {
                    response = "Player not found";
                    return false;
                }
            }

            // break up first argument into characters and map them onto pills
            // unforutately we can't do in a functional style due to C# limitations
            List<CustomPill> pills = new List<CustomPill> { };
            foreach (char character in arguments.First())
            {
                try
                {
                    var pill = SpawnItemUtils.CustomPills.First(p => char.ToUpper(character) == p.Letter);
                    pills.Add(pill);
                }
                catch
                {
                    response = "Invalid pill letter";
                    return false;
                }
            }

            foreach (CustomItem pill in pills)
            {
                if (player.IsInventoryFull)
                {
                    response = "Stopped due to full inventory";
                    return false;
                }
                pill.Give(player);
            }

            response = $"Gave {string.Join(", ", pills.Select(p => p.Name))}";
            return true;
        }
    }
}
