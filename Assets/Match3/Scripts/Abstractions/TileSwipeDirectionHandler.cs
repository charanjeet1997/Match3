namespace Games.Match3Game
{
    public abstract class TileSwipeDirectionHandler
    {
        public abstract Tile GetTileToSwap(Tile selectedTile, Tile[,] tiles, SwipeDirection swipeDirection);
    }
}