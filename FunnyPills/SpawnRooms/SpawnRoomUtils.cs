using Exiled.API.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace FunnyPills.SpawnRooms
{
    static internal class SpawnRoomUtils
    {
        /// <summary>
        ///  Spawn a pill in each of the spawn rooms
        /// </summary>
        public static void SpawnPills()
        {
            var customPillEnumerable = SpawnItemUtils.CustomPillsCircularEnumerator();
            foreach (SpawnRoom room in AllSpawnRooms)
            {
                var spawnpoint = room.GetRandomAbsoluteSpawnPoint();
                var item = customPillEnumerable.GetNext();
                item.Spawn(spawnpoint);
            }
        }


        /// <summary>
        ///  Spawn every pill in every room
        ///  (useful for debug)
        /// </summary>
        public static void SpawnEveryPossiblePill()
        {
            var customPillEnumerable = SpawnItemUtils.CustomPillsCircularEnumerator();
            foreach (SpawnRoom room in AllSpawnRooms)
            {
                foreach (Vector3 spawnpoint in room.AbsoluteSpawnPoints)
                {
                    var item = customPillEnumerable.GetNext();
                    item.Spawn(spawnpoint);
                }
            }
        }

        public static List<SpawnRoom> AllSpawnRooms = new List<SpawnRoom> {
            // SCP 914
            new SpawnRoom(RoomType.Lcz914, new List<Vector3>
            {
                // shelf left
                new Vector3(1, 3, -7),

                // shelf center
                new Vector3(0, 1, -7),
                new Vector3(0, 2, -7),

                // shelf right
                new Vector3(-0.8f, 3, -7),
                new Vector3(-.8f, 2, -7),
                new Vector3(-.8f, 1.5f, -7),

                // corners
                new Vector3(-2, 0.1f, -7.3f),
                new Vector3(-2, 0.1f, 7.3f),

                // left of 914
                new Vector3(4.3f, 0, 7.1f),

                // outside 914 room
                new Vector3(-7.2f, 0, -3.2f),
                new Vector3(-7.2f, 0, 3.2f),
            }),

            // SCP 939 CR
            new SpawnRoom(RoomType.Hcz939, new List<Vector3>
            {
                // top of lockers
                new Vector3(-6.2f, 2.3f, 0.1f),
                
                // left of lockers
                new Vector3(-6.4f, 0, -0.1f),

                // aqua room
                new Vector3(-1.7f, 0, -1.7f),
                
                // aqua room by pillar
                new Vector3(-6.4f, 0, -4.5f),

                // computer desk
                new Vector3(-2.1f, 1.1f, -1.0f),

                // by bottles on long desk
                new Vector3(-1.7f, 1.1f, 3.4f),

                // back right corner
                new Vector3(3.9f, 0, 6.5f),

                // behind beaker on small back left desk
                new Vector3(-6.3f, 1, 5),

                // on floor underneath center back desk
                new Vector3(-2.2f, 0, 6),

                // right of fridge
                new Vector3(-6, 0, 4.4f),
            }),

            // SCP 173 CR
            new SpawnRoom(RoomType.Lcz173, new List<Vector3>
            {
                // on top of lockers
                new Vector3(9.8f, 13.7f, 13.7f),

                // left of lockers
                new Vector3(8.6f, 11.5f, 13.8f),

                // corner next to locked door
                new Vector3(10.3f, 11.5f, -2.1f),

                // right of unlocked door
                new Vector3(1.7f, 11.5f, 2.2f),
                // right of unlocked door
                new Vector3(1.7f, 11.5f, -2.2f),

                // left of gate
                new Vector3(-1, 11.5f, -2.4f),

                // top of stairs
                new Vector3(-7.4f, 11.5f, -2.8f),

                // left of desk
                new Vector3(-2.3f, 11.4f, -3.1f),

                // the classic behind computer
                new Vector3(-2.3f, 12.3f, -6.6f),

                // in between gate and rail (diabolical)
                new Vector3(-2.0f, 11.4f, 2.2f)
            }),
        };
    }
}
