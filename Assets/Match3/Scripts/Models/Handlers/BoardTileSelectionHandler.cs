using Games.CameraManager;
using ServiceLocatorFramework;

namespace Games.Match3Game
{
	using UnityEngine;
	using System;
	using System.Collections;

	public class BoardTileSelectionHandler : IUpdater
	{

		#region PRIVATE_VARS
		private Camera mainCamera;
		private float lastTapTime;
		private float tapDelay = 0.1f; // Time delay to distinguish between tap and swipe
		private float tapStartTime;
		private BoardManager boardManager;
		private Match3GameConfiguration config;
		private BoosterManager boosterManager;
		private TileManager tileManager;
		Tile selectedTile;
		#endregion

		#region PUBLIC_VARS
		
		public Action<Tile> onTileSelected;
		#endregion

		#region PUBLIC_METHODS
		
		public BoardTileSelectionHandler(Match3GameConfiguration config)
		{
			this.config = config;
			this.boosterManager = ServiceLocator.Current.Get<BoosterManager>();
			boardManager = ServiceLocator.Current.Get<BoardManager>();
			tileManager = ServiceLocator.Current.Get<TileManager>();
			mainCamera = ServiceLocator.Current.Get<ICameraManager>().GetCamera();
		}

		public void OnUpdate()
		{
			if(Input.GetMouseButtonDown(0))
			{
				tapStartTime = Time.time;
				Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
				RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
				if(hit.collider != null)
				{
					Tile tile = hit.collider.GetComponent<Tile>();
					if(tile != null)
					{
						selectedTile = tile;
						if (boosterManager.selectedBooster != BoosterType.None)
						{
							boosterManager.UseBooster(tile, tileManager.Tiles);
						}
						onTileSelected?.Invoke(tile);
					}
				}
			}
			if(Input.GetMouseButtonUp(0))
			{
				if(Time.time - tapStartTime < tapDelay && selectedTile is PowerUpTile powerUpTile)
				{
					config.levelDatas[boardManager.match3GameConfiguration.selectedBoardIndex].challenge.UseMove();
					powerUpTile.UseBooster();
				}
				lastTapTime = Time.time;
			}
		}
		#endregion

		#region PRIVATE_METHODS

		#endregion

	}
}