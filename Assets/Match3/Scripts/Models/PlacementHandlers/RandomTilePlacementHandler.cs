using System;
using System.Collections.Generic;
using UnityEngine;

namespace Games.Match3Game
{
    public class RandomTilePlacementHandler : TilePlacementHandler
    {

        public override void HandlePlacement(int x, int y,TileType tileType,TileName tileName, Tile placedTile, TileData[] tilesData)
        {
            var neighbours = placedTile.neighbours;
            List<TileData> possibleTiles = new List<TileData>(Array.FindAll(tilesData,x=>x.tileName != TileName.Random && x.tileType == TileType.Normal));
          //  Debug.Log($"X : {x} Y : {y} has {neighbours.Count} neighbours.");
            for (int indexOfNeighbour = 0; indexOfNeighbour < neighbours.Count; indexOfNeighbour++)
            {
//                Debug.Log($"Tile X: {x} Y: {y} has neighbour X {neighbours[indexOfNeighbour].x} Y {neighbours[indexOfNeighbour].y} with tile type {neighbours[indexOfNeighbour].tileName.ToString()}.");
                for (int indexOfPossibleTile = 0; indexOfPossibleTile < tilesData.Length; indexOfPossibleTile++)
                {
                    if (tilesData[indexOfPossibleTile].tileName == neighbours[indexOfNeighbour].tileName)
                    {
                        TileData tileData = tilesData[indexOfPossibleTile];
                        bool isRemoved = possibleTiles.Remove(tileData);
                    }
                }
            }
            if (possibleTiles.Count == 0)
            {
                var data = Array.FindAll(tilesData, x => x.tileType == TileType.Normal);
                int randomIndex = UnityEngine.Random.Range(0, data.Length);
                placedTile.Init(x, y,tileType, data[randomIndex].tileName, data[randomIndex].tileIcon); 
            }
            else
            {
                int randomIndex = UnityEngine.Random.Range(0, possibleTiles.Count);
                placedTile.Init(x, y,tileType, possibleTiles[randomIndex].tileName, possibleTiles[randomIndex].tileIcon);
            }
        }
        
    }
}