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
                new Vector3(-2.297783f, 12.32794f, -6.601738f),

                // in between gate and rail (diabolical)
                new Vector3(-2.03199f, 11.4355f, 2.226341f),

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
                new Vector3(2.438232f, -1.42688f, -1.759621f),

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
                new Vector3(27.4f, -6.8f, -26.4f),

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
            }),


            // surface escape
            new SpawnRoom(RoomType.Surface, new List<Vector3>
            {
                // outer escape room inside:
                // outer escape door, inside, right side facing outside
                new Vector3(134.6f, -5.3f, -25.3f),
                // outer escape door, inside, left side facing outside
                new Vector3(138.2f, -5.3f, -25.3f),
                // inner escape door, inside square room, right side facing ramp
                new Vector3(132.8f, -5.3f, -18.6f),
                // inner escape door, inside square room, left side facing ramp
                new Vector3(134.6f, -5.3f, -18.6f),

                // resting on spike
                new Vector3(134.4f, -4.8f, -17.1f),

                // on ramp, left of openable door
                new Vector3(137.6f, -5.3f, -18),

                // surface across from exit, right corner
                new Vector3(130.8f, -5.3f, -66.7f),

                // surface across from exit, behind pillar
                new Vector3(141.8f, -5.5f, -59.6f)
            }),

            // surface exit building
            new SpawnRoom(RoomType.Surface, new List<Vector3>
            {
                // under table, left
                new Vector3(133.8f, -12.2f, 25.3f),

                // metal gate
                new Vector3(121.1f, -12.2f, 23.6f),

                // on terminal
                new Vector3(133.7f, -11.5f, 19.9f),

                // back corner
                new Vector3(133.1f, -12.2f, 25.9f)
            }),

            // surface upstairs
            new SpawnRoom(RoomType.Surface, new List<Vector3>
            {
                // behind railing near stairs by elevator A
                new Vector3(11.3f, 0.1f, -12.6f),

                // in front of railing near stairs by elevator A
                new Vector3(11.2f, 0, -13.2f),

                // cubby near elevator A
                new Vector3(-1.6f, 0, 7.2f),
            }),

            // 096 CR
            new SpawnRoom(RoomType.Hcz096, new List<Vector3>
            {
                // right of door
                new Vector3(-3.526001f, 0.03570557f, 1.396943f),

                // to the left of the corpse
                new Vector3(-0.3351747f, 0f, -0.7608796f),

                // back left
                new Vector3(-0.1967851f, 0f, 1.634773f),

                // left wall
                new Vector3(-1.854326f, 0f, 1.728248f),

                // outside cell, right of server
                new Vector3(-4.325073f, 0f, 2.704797f),

                // outside cell, left of terminal
                new Vector3(-6.599492f, 0f, 2.842536f),
            }),

            // old dog CR
            new SpawnRoom(RoomType.HczTestRoom, new List<Vector3>
            {
                // in the cup
                new Vector3(-0.4375801f, 0.7538452f, -4.826424f),

                // left windowsill
                new Vector3(-1.847164f, 0.8113403f, -4.208427f),

                // right of mic
                new Vector3(0.710762f, 0.7468872f, -4.924149f),

                // right of computer
                new Vector3(1.088753f, 0f, -4.538429f),

                // left of lamp
                new Vector3(-0.8975182f, 0.7468872f, -4.482376f),

                // behind computer
                new Vector3(0.3599701f, 0.7585449f, -4.502243f),
            }),

            // SCP 049 CR
            new SpawnRoom(RoomType.Hcz049, new List<Vector3>
            {
                // 2nd from bottom shelf, left
                new Vector3(4.872688f, 192.9505f, -6.998093f),

                // bottom shelf right
                new Vector3(3.741005f, 192.5399f, -7.059914f),
                
                // 3rd shelf left
                new Vector3(5.029716f, 193.3875f, -7.06649f),

                // 4th shelf middle
                new Vector3(4.388649f, 193.7803f, -6.994797f),

                // top right
                new Vector3(3.783722f, 194.2289f, -6.97905f),
            }),

            // SCP 106 CR set 1 (inside control room)
            new SpawnRoom(RoomType.Hcz106, new List<Vector3>
            {
                // left triangle structure
                new Vector3(23.98898f, 3.200745f, -5.564901f),

                // left shelf top left
                new Vector3(24.70265f, 2.151733f, -3.863644f),

                // bottom shelf left
                new Vector3(24.65559f, 0.4593506f, -3.846127f),
                
                // right of shelf
                new Vector3(24.68668f, -0.02410889f, -5.212471f),

                // top of terminal
                new Vector3(19.09278f, 2.139404f, -1.764769f),

                // top shelf right
                new Vector3(24.65392f, 1.703247f, -5.098042f),

                // left of femur breaker
                new Vector3(24.68352f, 0.0009155273f, -8.718685f),

                // right windowsill
                new Vector3(18.49769f, 0.9578247f, -6.666037f),
            }),

            // SCP 106 CR, set 2 (outside control room)
            new SpawnRoom(RoomType.Hcz106, new List<Vector3>
            {
                // at the path turn
                new Vector3(-5.027808f, 0.06201172f, -20.7898f),

                // behind coil 1 (closer to hcz)
                new Vector3(3.174925f, 0.8729858f, -12.67658f),

                // by coil 2
                new Vector3(9.117414f, 0.8729858f, -8.484824f),

                // on coil 1
                new Vector3(0.9208537f, 2.946228f, -10.79707f),

                // on coil 2
                new Vector3(0.82541f, 3.080994f, -10.52536f),

                // behind pillar 1 (closest to hcz)
                new Vector3(-2.11866f, 0.2909546f, -3.157971f),

                // behind railing near entrance door
                new Vector3(-7.354649f, 0.3106689f, -3.403337f),
            })

        };
    }
}
