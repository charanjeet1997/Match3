
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Games.UnnamedArcade3d.Entities.WorldSystem
{
	public class WorldGameoverStateProvider : IWorldStateProvider,IWorldSingleTick
	{
		public List<IGameOverState> gameOverStateEntities;

		public WorldGameoverStateProvider(List<IWorldEntity> worldEntities)
		{
			gameOverStateEntities = new List<IGameOverState>();

			foreach (var entity in worldEntities)
			{
				if (entity is IGameOverState)
				{
					gameOverStateEntities.Add((IGameOverState)entity);
				}
			}
		}

		public void Act(float transitionPercentage)
		{
			for (int indexOfInitState = 0; indexOfInitState < gameOverStateEntities.Count; indexOfInitState++)
			{
				gameOverStateEntities[indexOfInitState].GameOver();
			}
		}
	}
}