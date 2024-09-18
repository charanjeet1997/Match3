using System;
using System.Collections.Generic;
using UnityEngine;

namespace Games.UnnamedArcade3d.Entities.WorldSystem
{
	public class WorldGameplayStateProvider : IWorldStateProvider,IWorldContinousTick
	{
		public List<IWorldRunningState> runningStateEntities;

		public WorldGameplayStateProvider(List<IWorldEntity> worldEntities)
		{
			runningStateEntities = new List<IWorldRunningState>();

			foreach (var entity in worldEntities)
			{
				if (entity is IWorldRunningState)
				{
					runningStateEntities.Add((IWorldRunningState)entity);
				}
			}
		}

		public void Act(float transitionPercentage)
		{
			for (int indexOfInitState = 0; indexOfInitState < runningStateEntities.Count; indexOfInitState++)
			{
				runningStateEntities[indexOfInitState].Running();
			}
		}
	}
}