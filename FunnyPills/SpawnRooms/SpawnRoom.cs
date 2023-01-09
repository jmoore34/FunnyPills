using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FunnyPills.SpawnRooms
{
    internal class SpawnRoom
    {
        public SpawnRoom(Room room, List<Vector3> relativeSpawnPoints)
        {
            Room = room;
            RelativeSpawnPoints = relativeSpawnPoints;
        }

        Room Room { get; set; }

        /// <summary>
        ///  Room-locally positioned points where funny pills should spawn
        /// </summary>
        List<Vector3> RelativeSpawnPoints { get; set; }

        public IEnumerable<Vector3> AbsoluteSpawnPoints => RelativeSpawnPoints.Select(relative => Room.Transform.TransformPoint(relative));

        /// <summary>
        /// Return the absolute (relative to the entire map) position of one of the room's item spawn points
        /// </summary>
        /// <returns></returns>
        public Vector3 GetRandomAbsoluteSpawnPoint()
        {
            return Room.Transform.TransformPoint(RelativeSpawnPoints.RandomItem());
        }
        

    }
}
