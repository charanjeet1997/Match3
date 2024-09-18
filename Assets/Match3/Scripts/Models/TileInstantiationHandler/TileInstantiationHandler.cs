namespace Games.Match3Game
{
	using UnityEngine;
	using System;
	using System.Collections;

	public class TileInstantiationHandler
	{
		public virtual Tile InstantiateTile(Match3GameConfiguration config, Transform parent, Vector3 position, int x, int y,TileName tileName,TileType tileType,PowerUpType powerUpType)
		{
			Tile tile = GameObject.Instantiate(config.tilePrefab, position, Quaternion.identity, parent);
			Guid guid = Guid.NewGuid();
			tile.tileID = guid.ToString();
			tile.transform.position = position;
			tile.x = x;
			tile.y = y;
			tile.name = "Tile_" + x + "," + y;
			tile.SetPosition(position);
			tile.SetGridPosition(x,y);
			tile.tileName = tileName;
			tile.tileType = tileType;
			return tile;
		}

	}
	
	public class EmptyTileInstantiationHandler : TileInstantiationHandler
	{
		public override Tile InstantiateTile(Match3GameConfiguration config, Transform parent, Vector3 position, int x, int y,TileName tileName,TileType tileType,PowerUpType powerUpType)
		{
			EmptyTile emptyTile = GameObject.Instantiate(config.emptyTilePrefab, position, Quaternion.identity, parent);
			Guid guid = Guid.NewGuid();
			emptyTile.tileID = guid.ToString();
			emptyTile.transform.position = position;
			emptyTile.x = x;
			emptyTile.y = y;
			emptyTile.name = "Tile_" + x + "," + y;
			emptyTile.SetPosition(position);
			emptyTile.SetGridPosition(x,y);
			emptyTile.tileName = tileName;
			emptyTile.tileType = tileType;
			return emptyTile;
		}
	}

	public class PowerUpTileInstantiationHandler : TileInstantiationHandler
	{
		public override Tile InstantiateTile(Match3GameConfiguration config, Transform parent, Vector3 position, int x, int y,TileName tileName,TileType tileType,PowerUpType powerUpType)
		{
			Debug.Log("PowerUpTileInstantiationHandler");
			PowerUpTile powerUpTile = GameObject.Instantiate(config.powerUpTilePrefab, position, Quaternion.identity, parent);
			powerUpTile.transform.position = position;
			Guid guid = Guid.NewGuid();
			powerUpTile.tileID = guid.ToString();
			powerUpTile.x = x;
			powerUpTile.y = y;
			powerUpTile.name = "Tile_" + x + "," + y;
			powerUpTile.SetPosition(position);
			powerUpTile.SetGridPosition(x,y);
			powerUpTile.tileName = tileName;
			powerUpTile.tileType = tileType;
			powerUpTile.powerUpType = powerUpType;
			return powerUpTile;
		}
	}
	
}