namespace Games.Match3Game
{
    public class RightSwipeDirectionHandler : TileSwipeDirectionHandler
    {
        public override Tile GetTileToSwap(Tile selectedTile, Tile[,] tiles, SwipeDirection swipeDirection)
        {
            Tile tile = tiles[selectedTile.x + 1, selectedTile.y];
            return tile;
        }
    }
}