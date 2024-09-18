using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ServiceLocatorFramework
{
	// /// <summary>
 //    /// Simple service locator for <see cref="IGameService"/> instances.
 //    /// </summary>
 //    public class ServiceLocator
 //    {
 //        private ServiceLocator() { }
 //
 //        /// <summary>
 //        /// currently registered services.
 //        /// </summary>
 //        private readonly Dictionary<string, IGameService> services = new Dictionary<string, IGameService>();
 //
 //        /// <summary>
 //        /// Gets the currently active service locator instance.
 //        /// </summary>
 //        public static ServiceLocator Current { get; private set; }
 //
 //        /// <summary>
 //        /// Initalizes the service locator with a new instance.
 //        /// </summary>
 //        public static void Initiailze()
 //        {
 //            Current = new ServiceLocator();
 //        }
 //
 //        /// <summary>
 //        /// Gets the service instance of the given type.
 //        /// </summary>
 //        /// <typeparam name="T">The type of the service to lookup.</typeparam>
 //        /// <returns>The service instance.</returns>
 //        public T Get<T>() where T : IGameService
 //        {
 //            string key = typeof(T).Name;
 //            if (!services.ContainsKey(key))
 //            {
 //                Debug.LogError($"{key} not registered with {GetType().Name}");
 //                throw new InvalidOperationException();
 //            }
 //
 //            return (T)services[key];
 //        }
 //        
 //     
 //
 //        /// <summary>
 //        /// Registers the service with the current service locator.
 //        /// </summary>
 //        /// <typeparam name="T">Service type.</typeparam>
 //        /// <param name="service">Service instance.</param>
 //        public void Register<T>(T service) where T : IGameService
 //        {
 //            string key = typeof(T).Name;
 //            if (services.ContainsKey(key))
 //            {
 //                Debug.LogError($"Attempted to register service of type {key} which is already registered with the {GetType().Name}.");
 //                return;
 //            }
 //
 //            services.Add(key, service);
 //        }
 //
 //        /// <summary>
 //        /// Unregisters the service from the current service locator.
 //        /// </summary>
 //        /// <typeparam name="T">Service type.</typeparam>
 //        public void Unregister<T>() where T : IGameService
 //        {
 //            string key = typeof(T).Name;
 //            if (!services.ContainsKey(key))
 //            {
 //                Debug.LogError($"Attempted to unregister service of type {key} which is not registered with the {GetType().Name}.");
 //                return;
 //            }
 //
 //            services.Remove(key);
 //        }
 //    }
    
    /// <summary>
    /// Simple service locator for <see cref="IGameService"/> instances.
    /// </summary>
    public class ServiceLocator
    {
        private ServiceLocator() { }

        /// <summary>
        /// currently registered services.
        /// </summary>
        private readonly Dictionary<Type, object> services = new Dictionary<Type,object>();

        /// <summary>
        /// Gets the currently active service locator instance.
        /// </summary>
        public static ServiceLocator Current { get; private set; }

        /// <summary>
        /// Initalizes the service locator with a new instance.
        /// </summary>
        public static void Initiailze()
        {
            Current = new ServiceLocator();
        }

        /// <summary>
        /// Gets the service instance of the given type.
        /// </summary>
        /// <typeparam name="T">The type of the service to lookup.</typeparam>
        /// <returns>The service instance.</returns>
        public T Get<T>()
        {
            // string key = typeof(T).Name;
            if (!services.ContainsKey(typeof(T)))
            {
                Debug.LogError($"{typeof(T).Name} not registered with {GetType().Name}");
                throw new InvalidOperationException();
            }
            return (T)services[typeof(T)];
        }
        
        public bool Has<T>()
        {
            return services.ContainsKey(typeof(T));
        }
        
        /// <summary>
        /// Check and Gets the service instance of the given type.
        /// </summary>
        /// <typeparam name="T">The type of the service to lookup.</typeparam>
        /// <returns>The service instance.</returns>
        public bool IsExist<T>(out T t)
        {
            t = default;
            // string key = typeof(T).Name;
            if (!services.ContainsKey(typeof(T)))
            {
                return false;
            }
            t = (T) services[typeof(T)];
            return true;
        }
        
        
        
        /// <summary>
        /// Registers the service with the current service locator.
        /// </summary>
        /// <typeparam name="T">Service type.</typeparam>
        /// <param name="service">Service instance.</param>
        public void Register<T>(T service)
        {
            // string key = typeof(T).Name;
            if (services.ContainsKey(typeof(T)))
            {
                Debug.LogError($"Attempted to register service of type {typeof(T).Name} which is already registered with the {GetType().Name}.");
                return;
            }

            services.Add(typeof(T), service);
        }

        /// <summary>
        /// Unregisters the service from the current service locator.
        /// </summary>
        /// <typeparam name="T">Service type.</typeparam>
        public void Unregister<T>()
        {
            // string key = typeof(T).Name;
            if (!services.ContainsKey(typeof(T)))
            {
                Debug.LogError($"Attempted to unregister service of type {typeof(T).Name} which is not registered with the {GetType().Name}.");
                return;
            }
            services.Remove(typeof(T));
        }
    }
}