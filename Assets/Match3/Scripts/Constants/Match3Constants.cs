using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Games.Match3Game
{
    public static class Match3Constants
    {
        public static class AnimatorConstants
        {
            public static readonly string Shrink = "Shrink";
            public static readonly string Show = "Show";
            public static readonly string Vibrate = "Vibrate";
        }

        public static class PropertyNameConstants
        {
            // Match 3 Properties
             public static readonly string MovesPropertyName = "Moves";
            public static readonly string TileToCollect1PropertyName = "TileToCollect1";
            public static readonly string TileToCollect2PropertyName = "TileToCollect2";
            public static readonly string TileToCollect1SpritePropertyName = "TileToCollect1Sprite";
            public static readonly string TileToCollect2SpritePropertyName = "TileToCollect2Sprite";
            public static readonly string MaxTileToCollect1PropertyName = "MaxTileToCollect1";
            public static readonly string MaxTileToCollect2PropertyName = "MaxTileToCollect2";
            public static readonly string SFXVolumeProperty = "SFXVolume";
        }

        public static class Match3SaveAndLoadConstants
        {
            public static readonly string levelDirectoryName = "Match3Levels";
            public static readonly string levelDirectoryPath = Path.Combine(Application.streamingAssetsPath, levelDirectoryName);
            public static void SaveLevel(string json, int levelIndex)
            {
                if(!Directory.Exists(Match3SaveAndLoadConstants.levelDirectoryPath))
                {
                    Directory.CreateDirectory(Match3SaveAndLoadConstants.levelDirectoryPath);
                }
                string levelPath = Path.Combine(Match3SaveAndLoadConstants.levelDirectoryPath, $"Level_{levelIndex}");
                File.WriteAllText(levelPath, json);
            }
            
            public static string LoadLevel(int levelIndex)
            {
                string levelPath = Path.Combine(Match3SaveAndLoadConstants.levelDirectoryPath, $"Level_{levelIndex}");
                if(File.Exists(levelPath))
                {
                    return File.ReadAllText(levelPath);
                }
                return null;
            }
            
            public static string LoadLevelPath(int levelIndex)
            {
                return Path.Combine(Match3SaveAndLoadConstants.levelDirectoryPath, $"Level_{levelIndex}.dat");
            }
        }
        
        
    }
}
