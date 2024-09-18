namespace Games.Match3Game
{
	using System;
	using DataBindingFramework;
	using ServiceLocatorFramework;
	using UnityEngine;
	using System.Collections.Generic;

	public class BoardManager : MonoBehaviour
	{
		#region PRIVATE_VARS
		
		[SerializeField] private SwipeDetector swipeDetector;

		// Properties
		IPropertyManager propertyManager;
		Property<Sprite> targetTile1SpriteProperty;
		Property<Sprite> targetTile2SpriteProperty;
		
		private Tile selectedTile;
		// Handlers
		private SwapManager swapManager;
		private BoardTileSelectionHandler boardTileSelectionHandler;
		// Handlers
		private Dictionary<SwipeDirection,TileSwipeDirectionHandler> swipeDirectionHandlers;

		#endregion

		#region PUBLIC_VARS
		public Match3GameConfiguration match3GameConfiguration;
		// Properties

		#endregion

		#region UNITY_CALLBACKS

		private void OnEnable()
		{
			ServiceLocator.Current.Register(this);
		}
		
		private void Update()
		{
			if(boardTileSelectionHandler != null)
				boardTileSelectionHandler.OnUpdate();
		}

		private void OnDisable()
		{
			swipeDetector.OnSwipe -= OnTileSwiped;
			boardTileSelectionHandler.onTileSelected -= OnTileSelected;
			ServiceLocator.Current.Unregister<BoardManager>();
		}

		#endregion

		#region PUBLIC_METHODS

		public void ResetSwipe()
		{
			swipeDetector.ResetSwipe();
		}

		public void InitProperties()
		{
			CacheProperties();
		}
		public void StartGame()
		{
			match3GameConfiguration.SetConfiguration(match3GameConfiguration.selectedBoardIndex);
			swapManager = GetComponent<SwapManager>();
			boardTileSelectionHandler = new BoardTileSelectionHandler(match3GameConfiguration);
			boardTileSelectionHandler.onTileSelected += OnTileSelected;
			swipeDetector.OnSwipe += OnTileSwiped;
			InitSwipeDirectionHandlers();
			CacheProperties();
		}
		
		public void StopGame()
		{
			swipeDetector.OnSwipe -= OnTileSwiped;
		}
		
		public void ResumeGame()
		{
			swipeDetector.OnSwipe += OnTileSwiped;
		}
		
		public void UpdateProperties()
		{
			targetTile1SpriteProperty.Value = Array.Find(match3GameConfiguration.tilesData, tileData => tileData.tileName == match3GameConfiguration.Challenge.targetTile1.tileName).tileIcon;
			targetTile2SpriteProperty.Value = Array.Find(match3GameConfiguration.tilesData, tileData => tileData.tileName == match3GameConfiguration.Challenge.targetTile2.tileName).tileIcon;
			match3GameConfiguration.Challenge.UpdatePropertyData();
		}
		
		#endregion

		#region PRIVATE_METHODS
		
		private void InitSwipeDirectionHandlers()
		{
			swipeDirectionHandlers = new Dictionary<SwipeDirection, TileSwipeDirectionHandler>
			{
				{SwipeDirection.Left, new LeftSwipeDirectionHandler()}, {SwipeDirection.Right, new RightSwipeDirectionHandler()},
				{SwipeDirection.Up, new UpSwipeDirectionHandler()}, {SwipeDirection.Down, new DownSwipeDirectionHandler()}
			};
		}
		
		private void OnTileSwiped(SwipeDirection swipeDirection)
		{
			Tile tileToSwap = swipeDirectionHandlers[swipeDirection].GetTileToSwap(selectedTile, ServiceLocator.Current.Get<TileManager>().Tiles, swipeDirection);
			if (tileToSwap != null)
			{
				swapManager.SwapTiles(selectedTile, tileToSwap);
				return;
			}
			Debug.LogWarning("Tile to swap is null");
		}
		private void OnTileSelected(Tile selectedTile)
		{
			this.selectedTile = selectedTile;	
		}
		
		private void CacheProperties()
		{
			propertyManager = ServiceLocator.Current.Get<IPropertyManager>();
			targetTile1SpriteProperty = propertyManager.GetOrCreateProperty<Sprite>(Match3Constants.PropertyNameConstants.TileToCollect1SpritePropertyName);
			targetTile2SpriteProperty = propertyManager.GetOrCreateProperty<Sprite>(Match3Constants.PropertyNameConstants.TileToCollect2SpritePropertyName);
			UpdateProperties();
		}
		#endregion
	}
}