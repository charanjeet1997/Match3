namespace Games.Match3Game
{
    public abstract class TilePlacementHandler
    {
        public abstract void HandlePlacement(int x,int y,TileType tiletype,TileName tileName,Tile placedTile, TileData[] tilesData);
    }
}