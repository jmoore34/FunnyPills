using Exiled.CustomItems.API.Features;
using FunnyPills.Items;
using System.Collections.Generic;
using System.Linq;

namespace FunnyPills
{
    internal static class SpawnItemUtils
    {
        /// <summary>
        /// All custom pills objects than spawn
        /// </summary>
        public static CustomPill[] CustomPills { get; set; } = new CustomPill[]
        {
            (CustomPill) CustomItem.Get(SummonPills.ItemId),
            (CustomPill) CustomItem.Get(BetrayPills.ItemId),
            //(CustomPill) CustomItem.Get(DisguisePills.ItemId),
            //(CustomPill) CustomItem.Get(SizePills.ItemId),
            (CustomPill) CustomItem.Get(SpeedPills.ItemId),
            (CustomPill) CustomItem.Get(TeleportPills.ItemId),
            (CustomPill) CustomItem.Get(DoorExplodePills.ItemId),
        };

        public static string PillDescriptions =>
            string.Join("\n",
                CustomPills.Select(pill => $"<color=#5aeee7>{pill.Letter}</color> - {pill.Description}"));


        /// <summary>
        /// Enumerates through every pill type then wraps around
        /// </summary>
        /// <returns>a random custom pill</returns>
        public static IEnumerator<CustomItem> CustomPillsCircularEnumerator()
        {
            CustomPill[] shuffled = (CustomPill[])CustomPills.Clone();
            shuffled.ShuffleList();
            while (true)
                foreach (var pill in shuffled)
                    yield return pill;
        }

        /// <summary>
        /// Skips the safety check (since an infinite enumerator can never run out)
        /// and gives you the next item
        /// </summary>
        /// <param name="circularEnumerator">get from CustomPillsCircularEnumerator()</param>
        /// <returns>the next custom pill</returns>
        public static CustomItem GetNext(this IEnumerator<CustomItem> circularEnumerator)
        {
            circularEnumerator.MoveNext();
            return circularEnumerator.Current;
        }
    }
}
