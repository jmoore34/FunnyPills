﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FunnyPills
{
    public static class Util
    {
        /// <summary>
        /// Given an IEnumerable (like a list), get a random element
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">a list or other enumerable object</param>
        /// <returns>a random item from that enumerable</returns>
        public static T RandomElement<T>(this IEnumerable<T> enumerable)
        {
            int index = UnityEngine.Random.Range(0, enumerable.Count());
            return enumerable.ElementAt(index);
        }
    }
}