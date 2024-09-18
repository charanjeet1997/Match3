using System;
using System.Collections.Generic;
using UnityEngine;

namespace Games.UnnamedArcade3d.Entities.WorldSystem
{
    [CreateAssetMenu(menuName = "Data/Database/WorldDatabase", fileName = "WorldDatabase")]
    public class WorldDatabase : ScriptableObject
    {
        [Serializable]
        public class WorldData
        {
            [SerializeField] private WorldName worldName;

            [SerializeField] private World world;

            public WorldName WorldName
            {
                get { return worldName; }
            }

            public World World
            {
                get { return world; }
            }
        }

        [SerializeField] private List<WorldData> worlds;

        public WorldData GetWorld(WorldName worldName)
        {
            return worlds.Find(x => x.WorldName.Equals(worldName));
        }
    }
}
