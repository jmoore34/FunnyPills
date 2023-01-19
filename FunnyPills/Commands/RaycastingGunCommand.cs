using CommandSystem;
using Exiled.CustomItems.API.Features;
using FunnyPills.Items;
using System;

namespace FunnyPills.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class RaycastingGunCommand : ICommand
    {
        public string Command => "raytracinggun";
        public string[] Aliases => new[] { "rg" };
        public string Description => "Gives you a raycasting gun";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission(PlayerPermissions.GivingItems))
            {
                response = "You need item spawning perms in order to use this command.";
                return false;
            }

            var player = Exiled.API.Features.Player.Get(sender);
            CustomItem.Get(RaycastingGun.ItemId).Give(player);
            response = "Gave a raytracing gun";
            return true;
        }
    }
}
