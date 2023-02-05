using Exiled.CustomItems.API.Features;

namespace FunnyPills.Items
{
    internal abstract class CustomPill : CustomItem
    {
        abstract public char Letter { get; set; }

        // Message when player tries to use pills in pocket dimension
        public string PocketDimensionMessage => $"<color=#faa7b0>An anomolous force prevents the use of SCP 500-{Letter} here</color>";
        public const ushort PocketDimensionMessageDuration = 6;
    }
}
