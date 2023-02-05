using CommandSystem;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnyPills.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class SizeCommand : ICommand
    {
        public string Command => "size";
        public string[] Aliases => new[] { "s" };
        public string Description => "Change your scale (providing X, Y, and Z scale factors)";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            float x, y, z;
            try
            {
                x = float.Parse(arguments.First());
                y = float.Parse(arguments.Skip(1).First());
                z = float.Parse(arguments.Skip(2).First());
            }
            catch
            {
                response = "PLease provide x, y, and z scale factors";
                return false;
            }

            Player.Get(sender).Scale = new UnityEngine.Vector3(x, y, z);
            response = $"Set scale to x={x} y={y} z={z}";
            return true;
        }
    }
}
