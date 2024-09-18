using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Factories
{
    
    //This class will be used for unique factories with unique type. 
    public class FactoryManagerByType : MonoBehaviour
    {
        public static FactoryManagerByType Instance { get; private set; }

        // Dictionary to hold factories. Each factory must implement IFactory<MonoBehaviour>.
        // Separate dictionaries for type and name
        private Dictionary<Type, object> factoriesByType;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            factoriesByType = new Dictionary<Type, object>();
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        // Register a factory with its configuration
        public static void RegisterFactoryByType<T>(IFactory<T> factory, FactoryConfig<T> config)
        {
            if (Instance == null)
            {
                Debug.LogError("FactoryManager instance not found.");
                return;
            }
            Type type = typeof(T);

            if (Instance.factoriesByType.TryGetValue(type, out var existingFactory))
            {
                Debug.LogWarning($"Factory for type {type} already exists.");
                return;
            }
            factory.Configure(config, Instance, Instance.transform);
            Instance.factoriesByType[typeof(T)] = factory as IFactory<T>;
        }

        // Create an object of type T using the corresponding factory
        public static T Create<T>() where T : MonoBehaviour
        {
            if (Instance.factoriesByType.TryGetValue(typeof(T), out var factory))
            {
                if (((IFactory<T>)factory) == null)
                    Debug.LogError($"Factory not found for type: {typeof(T)}");


                return ((IFactory<T>)factory).Create();
            }

            Debug.LogError($"Factory not found for type: {typeof(T)}");
            return null;
        }
        
        //Return IFactory<T> for type T
        public static IFactory<T> GetFactory<T>() where T : MonoBehaviour
        {
            if (Instance.factoriesByType.TryGetValue(typeof(T), out var factory))
            {
                if (((IFactory<T>)factory) == null)
                    Debug.LogError($"Factory not found for type: {typeof(T)}");


                return ((IFactory<T>)factory);
            }
            else
            {
                Debug.LogError($"Factory not found for type: {typeof(T)}");
                return default;
            }
        }

        // Recycle an object of type T using the corresponding factory
        public static void Recycle<T>(T obj) where T : MonoBehaviour
        {
            if (Instance.factoriesByType.TryGetValue(typeof(T), out var factory))
            {
                ((IFactory<T>)factory).Recycle(obj);
            }
            else
            {
                Debug.LogError($"Factory not found for type: {typeof(T)}");
            }
        }

        // //Cleanup all factories
        public static void CleanupAllFactories()
        {
            foreach (var factoryPair in Instance.factoriesByType)
            {
                if (factoryPair.Value is IFactory factory)
                {
                    factory.Cleanup();
                }
                else
                {
                    Debug.LogError($"Factory for type: {factoryPair.Key} is not an IFactory");
                }
            }
            Instance.factoriesByType.Clear();
        }

        //Clean up factory of type T
        public static void CleanupFactory<T>() where T : MonoBehaviour
        {
            Type factoryType = typeof(T);
            if (Instance.factoriesByType.TryGetValue(factoryType, out var factory))
            {
                ((IFactory<T>)factory).Cleanup();
                Instance.factoriesByType.Remove(factoryType);

            }
            else
            {
                Debug.LogError($"Factory not found for type: {typeof(T)}");
            }
        }
    }
}