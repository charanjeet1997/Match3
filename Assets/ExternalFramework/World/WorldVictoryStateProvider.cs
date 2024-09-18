using System;
using System.Collections.Generic;
using UnityEngine;

namespace Games.UnnamedArcade3d.Entities.WorldSystem
{
	public class WorldVictoryStateProvider : IWorldStateProvider,IWorldContinousTick
	{
		public List<IVictoryState> runningStateEntities;

		public WorldVictoryStateProvider(List<IWorldEntity> worldEntities)
		{
			runningStateEntities = new List<IVictoryState>();

			foreach (var entity in worldEntities)
			{
				if (entity is IVictoryState)
				{
					runningStateEntities.Add((IVictoryState)entity);
				}
			}
		}

		public void Act(float transitionPercentage)
		{
			for (int indexOfInitState = 0; indexOfInitState < runningStateEntities.Count; indexOfInitState++)
			{
				runningStateEntities[indexOfInitState].Victory();
			}
		}
	}
}