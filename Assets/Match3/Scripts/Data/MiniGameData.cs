using System;
using System.Collections.Generic;
using UnityEngine;

namespace Games.Match3Game
{
    [Serializable]
    public class MiniGameData
    {
        public int levelIndex;
        
        public MiniGameData(int levelIndex)
        {
            this.levelIndex = levelIndex;
        }
    }
}