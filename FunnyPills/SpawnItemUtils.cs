using Exiled.CustomItems.API.Features;
using FunnyPills.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnyPills
{
    internal static class SpawnItemUtils
    {
        /// <summary>
        /// All custom pills objects than spawn
        /// </summary>
        public static List<int> CustomPillsIDs { get; set; } = new List<int>
        {
            TeleportPills.ItemId
        };

        /// <summary>
        /// Enumerates through every pill type then wraps around
        /// </summary>
        /// <returns>a random custom pill</returns>
        public static IEnumerator<CustomItem> CustomPillsCircularEnumerator()
        {
            CustomPillsIDs.ShuffleList();
            while (true)
                foreach (int id in CustomPillsIDs)
                    yield return CustomItem.Get(id);
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
