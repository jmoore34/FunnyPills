//using Exiled.API.Features;
//using Exiled.API.Features.Attributes;
//using Exiled.API.Features.Spawn;
//using Exiled.CustomItems.API.Features;
//using Exiled.Events.EventArgs.Player;
//using System.Collections.Generic;
//using UnityEngine;

//namespace FunnyPills.Items
//{
//    [CustomItem(ItemType.SCP500)]
//    internal class SizePills : CustomPill
//    {
//        public const int ItemId = 5006;
//        public override uint Id { get; set; } = ItemId;
//        public override char Letter { get; set; } = 'M';

//        public override string Name { get; set; } = "<color=#e370f3>SCP-500-M</color>";
//        public override string Description { get; set; } = "Randomly change your model size";
//        public override float Weight { get; set; } = 0;
//        public override SpawnProperties SpawnProperties { get; set; }
//        private Dictionary<Player, Vector3> affectedPlayers = new Dictionary<Player, Vector3>();

//        protected override void SubscribeEvents()
//        {
//            Exiled.Events.Handlers.Player.UsingItem += OnUsingItem;
//            Exiled.Events.Handlers.Player.UsedItem += OnUsedItem;
//            Exiled.Events.Handlers.Player.Spawned += OnSpawn;
//            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStarted;
//            base.SubscribeEvents();
//        }

//        protected override void UnsubscribeEvents()
//        {
//            Exiled.Events.Handlers.Player.UsingItem -= OnUsingItem;
//            Exiled.Events.Handlers.Player.UsedItem -= OnUsedItem;
//            Exiled.Events.Handlers.Player.Spawned -= OnSpawn;
//            Exiled.Events.Handlers.Server.RoundStarted -= OnRoundStarted;
//            base.UnsubscribeEvents();
//        }
//
//private void OnUsingItem(UsingItemEventArgs ev)
//{
//    // work only on these custom pills
//    if (!Check(ev.Item))
//        return;

//    // don't allow within pocket dimension
//    if (ev.Player.CurrentRoom.Type == RoomType.Pocket)
//    {
//        ev.Player.ShowHint(PocketDimensionMessage, PocketDimensionMessageDuration);
//        ev.IsAllowed = false;
//        return;
//    }
//}
//      
//        private void OnUsedItem(UsedItemEventArgs ev)
//        {
//            if (Check(ev.Item))
//            {
//// Disallow special pill effects in pocket dimension
//if (ev.Player.CurrentRoom.Type == RoomType.Pocket)
//{
//    ev.Player.Broadcast(PocketDimensionMessageDuration, PocketDimensionMessage);
//    return;
//}
//                var sizes = new Vector3[]
//                {
//                    // squished
//                    new Vector3(1.7f, 0.2f, 1f),
//                    // inverted
//                    new Vector3(1f, -1f, 1f),
//                    // wide
//                    new Vector3(1.8f, 1.1f, 0.2f),
//                    // tall
//                    new Vector3(1.2f, 1.8f, 1.05f),
//                };
//                var scale = sizes.RandomElement();
//                ev.Player.Scale = scale;
//                affectedPlayers[ev.Player] = scale;
//            }
//        }

//        private void OnSpawn(SpawnedEventArgs ev)
//        {
//            if (affectedPlayers == null)
//            {
//                affectedPlayers = new Dictionary<Player, Vector3>();
//            }
//            if (ev != null && ev.Player != null && affectedPlayers.ContainsKey(ev.Player))
//            {
//                ev.Player.Scale = new Vector3(1f, 1, 1);
//                affectedPlayers.Remove(ev.Player);
//            }
//        }

//        private void OnRoundStarted()
//        {
//            affectedPlayers.Clear();
//        }
//    }
//}

