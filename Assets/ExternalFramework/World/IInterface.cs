using System;
using System.Collections.Generic;
using UnityEngine;

namespace Games.UnnamedArcade3d.Entities.WorldSystem
{
	public interface IWorldTick
	{
		
	}
	
	public interface IWorldContinousTick : IWorldTick
	{
	}
	
	public interface IWorldSingleTick : IWorldTick
	{
		
	}

	public interface IWorldState
	{
		
	}
	
	public interface IWorldRunningState : IWorldState
	{
		void Running();
	}
	public interface IWorldInitState : IWorldState
	{
		void Init();
	}
	public interface IWorldPauseState : IWorldState
	{
		void Pause();
	}

	public interface IGameOverState : IWorldState
	{
		void GameOver();
	}
	
	public interface IVictoryState : IWorldState
	{
		void Victory();
	}

	public interface IEditorLoadState : IWorldState
	{
		void Load();
	}

	public interface IEditorUnloadState : IWorldState
	{
		void Unload();
	}

	public interface IEditorInitState : IWorldState
	{
		void Init();
	}

	public interface IWorldCreatedState : IWorldState
	{
		void WorldCreated();
	}
	
	public interface IWorldDestroyedState : IWorldState
	{
		void WorldDestroyed();
	}
	public interface IEditorRunningState : IWorldState
	{
		void Running();
	}
	
	public interface IWorldStateProvider
	{
		void Act(float transitionPercentage);
	}

	public interface IWorldEntity
	{
		
	}
	public interface IWorldManager
	{
		World GetCurrentWorld();
		IWorldState GetCurrentWorldStatus();
		void CreateWorld(WorldDatabase.WorldData worldData);
		void CreateWorld(WorldName worldName);
		void DestroyWorld(WorldName worldName);
		void ChangeWorldStateTo(float transitionTime,IWorldStateProvider worldStateProvider);

		void RegisterEntityToCurrentWorld(IWorldEntity worldEntity);
		void UnregisterEntityToCurrentWorld(IWorldEntity worldEntity);

		
		
		List<IWorldEntity> GetCurrentWorldEntity();
	}
}