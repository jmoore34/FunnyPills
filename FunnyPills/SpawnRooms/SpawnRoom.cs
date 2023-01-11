using Exiled.API.Enums;
using Exiled.API.Features;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FunnyPills.SpawnRooms
{
    internal class SpawnRoom
    {
        public SpawnRoom(RoomType room, List<Vector3> relativeSpawnPoints)
        {
            RoomType = room;
            RelativeSpawnPoints = relativeSpawnPoints;
        }

        /// <summary>
        /// An enum for which room (e.g. 914) the spawn room is
        /// </summary>
        RoomType RoomType { get; set; }

        /// <summary>
        /// The actual Room object
        /// Must be accessed no sooner than map load
        /// </summary>
        Room Room => Exiled.API.Features.Room.Get(RoomType);

        /// <summary>
        ///  Room-locally positioned points where funny pills should spawn
        /// </summary>
        List<Vector3> RelativeSpawnPoints { get; set; }

        public IEnumerable<Vector3> AbsoluteSpawnPoints => RelativeSpawnPoints.Select(relative => Room.Get(RoomType).Transform.TransformPoint(relative));

        /// <summary>
        /// Return the absolute (relative to the entire map) position of one of the room's item spawn points
        /// </summary>
        /// <returns></returns>
        public Vector3 GetRandomAbsoluteSpawnPoint()
        {
            // remove eventually
            if (RoomType == null)
            {
                Log.Error("GetRandomAbsoluteSpawnPoint: Room is null");
            }
            else if (Room.Transform == null)
            {
                Log.Error("GetRandomAbsoluteSpawnPoint: Room.Transform is null");
            }
            return Room.Transform.TransformPoint(RelativeSpawnPoints.RandomItem());
        }
    }
}
