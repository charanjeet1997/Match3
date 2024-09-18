namespace Games.Match3Game
{
	using UnityEngine;
	using System;
	using System.Collections;

	public class SwapManager : MonoBehaviour
	{

		#region PRIVATE_VARS

		[SerializeField] private Match3GameConfiguration match3GameConfiguration;
		private BoardManager boardManager;
		private TileManager tileManager;
		int tempTile1X, tempTile1Y, tempTile2X, tempTile2Y;
		private Tile tile1, tile2;
		private Coroutine tile1Coroutine;
		private Coroutine tile2Coroutine;
		float centerX, centerY;
		bool isResetStarted = false;
		bool isTile1PowerUp = false, isTile2PowerUp = false;
		bool isMoveUsed = false;
		#endregion

		#region PUBLIC_VARS

		#endregion

		#region UNITY_CALLBACKS

		private void Start()
		{
			boardManager = GetComponent<BoardManager>();
			tileManager = GetComponent<TileManager>();
			isTile1PowerUp = isTile2PowerUp = false;
			centerX = (match3GameConfiguration.Width - 1f) / 2f;
			centerY = (match3GameConfiguration.Height - 1f) / 2f;
		}

		#endregion

		#region PUBLIC_METHODS

		public void SwapTiles(Tile tile1, Tile tile2)
		{
			StartCoroutine(AnimateSwap(tile1, tile2));
		}

		#endregion

		#region PRIVATE_METHODS

		private IEnumerator AnimateSwap(Tile tile1, Tile tile2)
		{

			if (tile1Coroutine == null && tile2Coroutine == null)
			{
				SetTiles(tile1, tile2);
				SetTempTileValues(tile1, tile2);
				SetTileArrayValues(tile1, tile2);
				SetTilesName(tile1, tile2);

				//Animate Tiles
				Vector2 tile1Position = new Vector2((tempTile2X - centerX) * match3GameConfiguration.TileGap, (tempTile2Y - centerY) * match3GameConfiguration.TileGap) + (Vector2)tileManager.TileOffset;
				Vector2 tile2Position = new Vector2((tempTile1X - centerX) * match3GameConfiguration.TileGap, (tempTile1Y - centerY) * match3GameConfiguration.TileGap) + (Vector2)tileManager.TileOffset;
				tile1Coroutine = tile1.AnimateToPosition(tile1Position, () => { tile1Coroutine = null; });
				tile2Coroutine = tile2.AnimateToPosition(tile2Position, () => { tile2Coroutine = null; });
				SwapTileValues(tile1, tile2);
			}

			yield return tile1Coroutine;
			yield return tile2Coroutine;

			if(tile1 is PowerUpTile powerUpTile1)
			{
				isTile1PowerUp = true;
				powerUpTile1.UseBooster();
			}

			if (tile2 is PowerUpTile powerUpTile)
			{
				isTile2PowerUp = true;
				powerUpTile.UseBooster();
			}
			bool isMatchFound = boardManager.GetComponent<TileMatchManager>().HandleMatches(tile1) && !isResetStarted || isTile1PowerUp;
			if (isMatchFound)
			{
				isMoveUsed = true;
				Debug.Log("Match Found");
			}

			bool isMatchFound2 = boardManager.GetComponent<TileMatchManager>().HandleMatches(tile2) && !isResetStarted || isTile2PowerUp;
			if (isMatchFound2)
			{
				isMoveUsed = true;
				Debug.Log("Match Found");
			}

			
			if(isMoveUsed)
			{
				isMoveUsed = false;
				yield return new WaitForSeconds(0.5f);
				boardManager.match3GameConfiguration.levelDatas[boardManager.match3GameConfiguration.selectedBoardIndex].challenge.UseMove();
			}
			if (!isMatchFound && !isMatchFound2 && !isResetStarted)
			{
				// Reset the tiles
				if (!isResetStarted)
				{
					yield return new WaitForSeconds(0.1f);
					isResetStarted = true;
					yield return StartCoroutine(AnimateSwap(tile1, tile2));
					boardManager.ResetSwipe();
				}
			}
			else
			{
				isResetStarted = false;
			}
		}


		private void SetTiles(Tile tile1, Tile tile2)
		{
			this.tile1 = tile1;
			this.tile2 = tile2;
		}

		private void SetTempTileValues(Tile tile1, Tile tile2)
		{
			tempTile1X = tile1.x;
			tempTile1Y = tile1.y;
			tempTile2X = tile2.x;
			tempTile2Y = tile2.y;
		}

		private void SetTilesName(Tile tile1, Tile tile2)
		{
			tile1.name = "Tile " + tempTile2X + ", " + tempTile2Y;
			tile2.name = "Tile " + tempTile1X + ", " + tempTile1Y;
		}

		private void SwapTileValues(Tile tile1, Tile tile2)
		{
			tile1.SetGridPosition(tile2.x, tile2.y);
			tile2.SetGridPosition(tempTile1X, tempTile1Y);
			tile1.x = tempTile2X;
			tile1.y = tempTile2Y;
			tile2.x = tempTile1X;
			tile2.y = tempTile1Y;
		}

		private void SetTileArrayValues(Tile tile1, Tile tile2)
		{
			tileManager.Tiles[tempTile1X, tempTile1Y] = tile2;
			tileManager.Tiles[tempTile2X, tempTile2Y] = tile1;
		}

		#endregion

	}
}