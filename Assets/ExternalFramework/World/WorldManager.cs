using System;
using System.Collections.Generic;
using Games.UnnamedArcade3d.Entities.LittleRed;
using ServiceLocatorFramework;
using UnityEngine;

namespace Games.UnnamedArcade3d.Entities.WorldSystem
{
	public class WorldManager : MonoBehaviour,IWorldManager
	{
		#region STATIC_FIELDS
		public static WorldManager Instance;
		#endregion
		#region PUBLIC_VARS
		[SerializeField]
		private WorldDatabase _worldDatabase;
		
		[SerializeField]
		private Dictionary<WorldName, World> worlds;
		#endregion

		#region PRIVATE_VARS
		private World currentWorld;
		#endregion

		#region UNITY_CALLBACKS
		private void Awake()
		{
			Instance = this;
		}

		private void OnEnable()
		{
			ServiceLocator.Current.Register<IWorldManager>(this);
		}

		private void OnDisable()
		{
			ServiceLocator.Current.Unregister<IWorldManager>();			
		}

		private void Start()
		{
			worlds = new Dictionary<WorldName, World>();
		}
		#endregion

		#region PUBLIC_METHODS
		public World GetCurrentWorld()
		{
			return currentWorld;
		}
		public IWorldState GetCurrentWorldStatus()
		{
			return (IWorldState) currentWorld;
		}
		public void CreateWorld(WorldDatabase.WorldData worldData)
		{
			if (!worlds.ContainsKey(worldData.WorldName))
			{
				currentWorld = Instantiate(worldData.World);
				worlds.Add(worldData.WorldName,currentWorld);
			}
		}

		public void CreateWorld(WorldName worldName)
		{
			if (!worlds.ContainsKey(worldName))
			{
				WorldDatabase.WorldData worldData = _worldDatabase.GetWorld(worldName);
				if (worldData != null)
				{
					currentWorld = Instantiate(worldData.World);
					worlds.Add(worldData.WorldName,currentWorld);
				}
				else
				{
					Debug.LogError("Requested World is not in the database");
				}
			}
			else
			{
				Debug.LogWarning("Requested World is already exist");				
			}
		}
		public void DestroyWorld(WorldName worldName)
		{
			if (worlds.ContainsKey(worldName))
			{
				World world = worlds[worldName];
				worlds.Remove(worldName);
				world.ClearAllEntities();
				Destroy(world.gameObject);
			}
		}

		public void ChangeWorldStateTo(float transitionTime,IWorldStateProvider worldStateProvider)
		{
			currentWorld.TransitionTo(transitionTime, worldStateProvider);
		}

		public List<IWorldEntity> GetCurrentWorldEntity()
		{
			return currentWorld.GetWorldEntities();
		}

		public void RegisterEntityToCurrentWorld(IWorldEntity worldEntity)
		{
			if(currentWorld==null)
				return;

			currentWorld.Add(worldEntity);
		}

		public void UnregisterEntityToCurrentWorld(IWorldEntity worldEntity)
		{
			if(currentWorld==null)
				return;
			currentWorld.Remove(worldEntity);
		}
		#endregion

		#region PRIVATE_METHODS

		#endregion
	}
}