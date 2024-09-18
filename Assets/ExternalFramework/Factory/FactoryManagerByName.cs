using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Factories
{
    
    //This class will be used for factories with unique name but can have redundant types.
    public class FactoryManagerByName : MonoBehaviour
    {
        public static FactoryManagerByName Instance { get; private set; }

        // Dictionary to hold factories. Each factory must implement IFactory<MonoBehaviour>.
        // Separate dictionaries for type and name
        private Dictionary<string, object> factoriesByName;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            factoriesByName = new Dictionary<string, object>();
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

     
        
        // Register a factory with name
        public static void RegisterFactoryByName<T>(IFactory<T> factory, FactoryConfig<T> config) 
        {
            if (Instance == null)
            {
                Debug.LogError("FactoryManager instance not found.");
                return;
            }

            // Type type = typeof(T);
            // Debug.Log("Prefab name : "+config.prefab.ToString());
            if (Instance.factoriesByName.TryGetValue(config.name, out var existingFactory))
            {
                return;
            }
            
            factory.Configure(config, Instance, Instance.transform);
            Instance.factoriesByName[factory.Name] = factory as IFactory<T>;
        }
        
        //Create an object of type T using factory name
        public static T Create<T>(string factoryName)
        {
            if (Instance.factoriesByName.TryGetValue(factoryName, out var factory))
            {
                if (factory is IFactory<T> typedFactory)
                {
                    return typedFactory.Create();
                }
            }

            Debug.LogError($"Factory not found for name: {factoryName}");
            return default;
        }

        public static void Recycle<T>(T obj, string factoryName)
        {
            if (Instance.factoriesByName.TryGetValue(factoryName, out var factory))
            {
                if (factory is IFactory<T> typedFactory)
                {
                    typedFactory.Recycle(obj);
                }
                else
                {
                    Debug.LogError($"Factory not found for name: {factoryName}");
                }
            }
            else
            {
                Debug.LogError($"Factory not found for name: {factoryName}");
            }
        }
        

        // //Cleanup all factories
        public static void CleanupAllFactories()
        {
            foreach (var factoryPair in Instance.factoriesByName)
            {
                if (factoryPair.Value is IFactory factory)
                {
                    factory.Cleanup();
                }
                else
                {
                    Debug.LogError($"Factory for name: {factoryPair.Key} is not an IFactory");
                }
            }
            Instance.factoriesByName.Clear();
        }
        
        // Get the factory by name
        public static IFactory<T> GetFactory<T>(string factoryName)
        {
            if (Instance.factoriesByName.TryGetValue(factoryName, out var factory))
            {
                if (factory is IFactory<T> typedFactory)
                {
                    return typedFactory;

                }
                else
                {
                    Debug.LogError($"Factory for name '{factoryName}' is not of type IFactory<{typeof(T)}>.");
                    return null;
                }
            }
            else
            {
                Debug.LogError($"Factory not found for name: {factoryName}");
                return null;
            }
        }

        //Clean up factory of name
        public static void CleanupFactory<T>(string factoryName) 
        {
            if (Instance.factoriesByName.TryGetValue(factoryName, out var factory))
            {
                if (factory is IFactory<T> typedFactory)
                {
                    typedFactory.Cleanup();
                    Instance.factoriesByName.Remove(factoryName);
                    // Debug.Log($"Factory for name: {factoryName} cleaned up.");
                }
                else
                {
                    Debug.LogError($"Factory not found for name: {factoryName}");
                }
            }
            else
            {
                Debug.LogError($"Factory not found for name: {factoryName}");
            }
        }
    }
}