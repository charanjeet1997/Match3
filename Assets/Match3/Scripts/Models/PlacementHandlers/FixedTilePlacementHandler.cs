using System;

namespace Games.Match3Game
{
    public class FixedTilePlacementHandler : TilePlacementHandler
    {
        public override void HandlePlacement(int x, int y,TileType type,TileName tileName, Tile placedTile, TileData[] tilesData)
        {
            placedTile.Init(x, y, type,tileName, Array.Find(tilesData, t => t.tileName == tileName).tileIcon);
        }
    }
}