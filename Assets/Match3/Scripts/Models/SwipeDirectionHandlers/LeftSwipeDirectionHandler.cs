using UnityEngine;

namespace Games.Match3Game
{
    public class LeftSwipeDirectionHandler : TileSwipeDirectionHandler
    {
        public override Tile GetTileToSwap(Tile selectedTile, Tile[,] tiles, SwipeDirection swipeDirection)
        {
            Debug.Log("Left swipe direction handler called");
            return tiles[selectedTile.x - 1, selectedTile.y];
        }
    }
}