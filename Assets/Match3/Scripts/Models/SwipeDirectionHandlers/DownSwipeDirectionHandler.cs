using UnityEngine;

namespace Games.Match3Game
{
    public class DownSwipeDirectionHandler : TileSwipeDirectionHandler
    {
        public override Tile GetTileToSwap(Tile selectedTile, Tile[,] tiles, SwipeDirection swipeDirection)
        {
            Debug.Log("Down swipe direction handler called");
            return tiles[selectedTile.x, selectedTile.y - 1];
        }
    }
}