using System;
using System.Collections.Generic;
using UnityEngine;

namespace Games.UnnamedArcade3d.Entities.WorldSystem
{
	public class WorldInitStateProvider : IWorldStateProvider,IWorldSingleTick
	{
		public List<IWorldInitState> initStateEntities;
		public WorldInitStateProvider(List<IWorldEntity> worldEntities)
		{
			initStateEntities = new List<IWorldInitState>();

			foreach (var entity in worldEntities)
			{
				if (entity is IWorldInitState)
				{
					initStateEntities.Add((IWorldInitState)entity);
				}
			}
		}

		public void Act(float transitionPercentage)
		{
			for (int indexOfInitState = 0; indexOfInitState < initStateEntities.Count; indexOfInitState++)
			{
				initStateEntities[indexOfInitState].Init();
			}
		}
	}
}