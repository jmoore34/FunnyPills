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
                new Vector3(-2.0f, 11.4f, 2.1f),

                // bottom floor behind pole
                new Vector3(-6.3f, 0, -1.8f)
            }),

            // WC
            new SpawnRoom(RoomType.LczToilets, new List<Vector3>
            {
                // men's sink
                new Vector3(5.5f, 1, -6.3f),

                // under men's sink
                new Vector3(5.7f, 0, -6.8f),

                // women's toilet
                new Vector3(-0.4f, 1.3f, -7),

                // women's sink
                new Vector3(-5.6f, 1, -6),

                // under stall, impossible to pick up due to invisible wall
                new Vector3(1.9f, 0.1f, -5.2f)  
            }),

            // 079 CR
            new SpawnRoom(RoomType.Hcz079, new List<Vector3>
            {
                // classic left stairs bottom
                new Vector3(5.4f, -3.1f, 1.7f),

                // mid stairs right
                new Vector3(2.4f, -1.4f, -1.8f),

                // right of regular door
                new Vector3(-7.2f, 0.1f, -1.6f)
            }),

            // intercom
            new SpawnRoom(RoomType.EzIntercom, new List<Vector3>
            {
                // right of intercom console, on floor
                new Vector3(-6.3f, -5.8f, -4.2f),

                // behind lamp
                new Vector3(-6.7f, -5, -3.9f),

                // on desk
                new Vector3(-6.9f, -5, -2.5f),

                // in the hidey spot
                new Vector3(-5.2f, -5.8f, 3.4f),

                // middle of stairs
                new Vector3(4.5f, -1.5f, 1.1f)
            }),

            // surface nuke room
            new SpawnRoom(RoomType.Surface, new List<Vector3>
            {
                // right of nuke console
                new Vector3(31.7f, -9.1f, -23.8f),

                // left of left table
                new Vector3(27.1f, -8.9f, -25.6f),

                // right of workstation
                new Vector3(31.6f, -8, -28.6f),

                // top of left machine
                new Vector3(27.4f, -6.8f, -26.4f)

                // top of nuke terminal
                new Vector3(29.6f, -6.8f, -23.6f),

                // top of nuke teminal, back right
                new Vector3(30.2f, -6.8f, -23.3f)
            }),


            // chaos spawn part of surface
            new SpawnRoom(RoomType.Surface, new List<Vector3>
            {
                // wall closer to elevators, behind pillar
                new Vector3(-14.1f, -9.1f, -34.6f),

                // right of locked door
                new Vector3(-41.2f, -9.1f, -35),

                // left of locked door
                new Vector3(-41.2f, -9.1f, -37.4f),

                // by last white stripes on pavement
                new Vector3(-37.2f, -12.6f, -42.8f)
            })

        };
    }
}
