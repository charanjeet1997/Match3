
using System;

namespace Games.Match3Game
{
	using UnityEngine;
	using ServiceLocatorFramework;

	public class GameManager : MonoBehaviour
	{

		#region PRIVATE_VARS
		[SerializeField] private Match3GameConfiguration config;
		[SerializeField] GameState gameState = GameState.NotStarted;
		private BoardManager boardManager;
		private TileManager tileManager;
		private TilePoolManager tilePoolManager;
		bool isGameEnded = false;
		#endregion

		#region PUBLIC_VARS

		public static Action OnGameWon = delegate {  };
		public static Action OnGameLose = delegate {  };
		public static Action<GameState> OnGameOver = delegate {  };
		#endregion

		#region UNITY_CALLBACKS

		private void OnEnable()
		{
			ServiceLocator.Current.Register(this);
		}

		private void Start()
		{
			boardManager = GetComponent<BoardManager>();
			tileManager = GetComponent<TileManager>();
			tilePoolManager = GetComponent<TilePoolManager>();
			gameState = GameState.NotStarted;
			int levelIndex = ServiceLocator.Current.Has<MiniGameData>() ? ServiceLocator.Current.Get<MiniGameData>().levelIndex : 0;
			Debug.Log("Level Index: " + levelIndex);
			config.SetConfiguration(levelIndex);
			CoroutineExtension.Execute(this, StartGame,1.2f);
		}

		private void Update()
		{
			if (gameState == GameState.Playing)
			{
				CheckGameProgress();
			}
		}

		private void OnDisable()
		{
			ServiceLocator.Current.Unregister<GameManager>();
		}

		#endregion

		#region PUBLIC_METHODS
		public void StartGame()
		{
			int levelIndex = ServiceLocator.Current.Has<MiniGameData>() ? ServiceLocator.Current.Get<MiniGameData>().levelIndex : 0;
			isGameEnded = false;
			config.SetConfiguration(levelIndex);
			CoroutineExtension.Execute(this, () =>
			{
				boardManager.StartGame();
				tilePoolManager.ClearTilePool();
				tilePoolManager.ClearPowerUpPool();
				tileManager.Initialize();
				CoroutineExtension.Execute(this, () => config.Challenge.Init(), 0.2f);
				CoroutineExtension.Execute(this, () => gameState = GameState.Playing, 0.5f);
			}, 0.5f);
		}
		public void PauseGame()
		{
			boardManager.StopGame();
			gameState = GameState.Paused;
		}
		
		public void RestartGame()
		{
			boardManager.StopGame();
			StartGame();
		}
		
		public void EndGame()
		{
			boardManager.StopGame();
			CoroutineExtension.Execute(this, () =>
			{
				tileManager.DestroyAllTiles();
				tilePoolManager.ClearTilePool();
				tilePoolManager.ClearPowerUpPool();
				switch (GameState.Won)
				{
					case GameState.Won:
						OnGameWon?.Invoke();
						break;
					case GameState.Lost:
						OnGameLose?.Invoke();
						break;
				}
				CoroutineExtension.Execute(this, () =>
				{
					boardManager.UpdateProperties();
				},0.2f);
			}, 1.5f);
		}
		
		#endregion

		#region PRIVATE_METHODS
		private void CheckGameProgress()
		{
			if (!isGameEnded)
			{
				if (config.Challenge.IsCompleted())
				{
					EndGame();
					gameState = GameState.Won;
					isGameEnded = true;
					Debug.Log("Game Won");
				}

				if (config.Challenge.IsFailed())
				{
					EndGame();
					gameState = GameState.Lost;
					isGameEnded = true;
					Debug.Log("Game Over");
				}
			}
		}
		#endregion
		
	}

	public enum GameState
	{
		NotStarted,
		Playing,
		Paused,
		Won,
		Lost
	}
	
}