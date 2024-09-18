namespace ServiceLocatorFramework
{
	using DataBindingFramework;
	using Game.Factories;
	using Games.CameraManager;
	using UnityEngine;

	public static class Bootstrapper
	{
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		public static void InitiailzeBeforeSceneLoad()
		{
			// Debug.Log("Initialized");
			// Initialize default service locator.
			ServiceLocator.Initiailze();
			ServiceLocator.Current.Register<IPropertyManager>(new PropertyManager());
			ServiceLocator.Current.Register<IObserverManager>(new ObserverManager());
			ServiceLocator.Current.Register<ICameraManager>(new CameraManager());
		}
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		public static void InitializeAfterSceneLoad()
		{
			GenerateFactoryManager();
		}

		private static void GenerateFactoryManager()
		{
			GameObject factoryManager = new GameObject("FactoryManager");
			factoryManager.AddComponent<FactoryManagerByType>();
			factoryManager.AddComponent<FactoryManagerByName>();
		}
	}
}