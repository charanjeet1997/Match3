using UnityEngine;

namespace Games.Match3Game
{
    public class UpSwipeDirectionHandler : TileSwipeDirectionHandler
    {
        public override Tile GetTileToSwap(Tile selectedTile, Tile[,] tiles, SwipeDirection swipeDirection)
        {
            Debug.Log("Up swipe direction handler called");
            return tiles[selectedTile.x, selectedTile.y + 1];
        }
    }
}