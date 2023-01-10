using Exiled.CustomItems.API.Features;

namespace FunnyPills.Items
{
    internal abstract class CustomPill : CustomItem
    {
        abstract public char Letter { get; set; }
    }
}
