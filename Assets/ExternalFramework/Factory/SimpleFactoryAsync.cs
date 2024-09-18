// using System.Collections;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using UnityEngine;
//
// namespace Game.Factories
// {
//     public class SimpleFactoryAsync<T> where T : Component, IManagedLifecycle
//     {
//         private Queue<T> availableObjects = new Queue<T>();
//         private T prefab;
//         private int maxCapacity;
//         private Transform parentTransform;
//         private bool startImmediately;
//         private int numberOfObjectsToCreate;
//         private float delayBetweenInstances;
//
//         public SimpleFactoryAsync(T prefab, int maxCapacity = 50, Transform parent = null, 
//                              bool startImmediately = true, int numberOfObjectsToCreate = 10,
//                              float delayBetweenInstances = 0.1f)
//         {
//             this.prefab = prefab;
//             this.maxCapacity = maxCapacity;
//             this.parentTransform = parent;
//             this.startImmediately = startImmediately;
//             this.numberOfObjectsToCreate = numberOfObjectsToCreate;
//             this.delayBetweenInstances = delayBetweenInstances;
//
//             if (startImmediately)
//             {
//                 Task.Run(() => CreateObjectsOverTimeAsync(numberOfObjectsToCreate, delayBetweenInstances));
//             }
//         }
//
//         public async Task InitializeAsync(int initialCapacity)
//         {
//             for (int i = 0; i < initialCapacity; i++)
//             {
//                 var obj = AddObject();
//                 obj.gameObject.SetActive(false); // Initialize in an inactive state
//                 availableObjects.Enqueue(obj);
//             }
//         }
//
//         private T AddObject()
//         {
//             var newObject = Object.Instantiate(prefab, parentTransform);
//             newObject.gameObject.SetActive(false);
//             return newObject;
//         }
//
//         public T Create()
//         {
//             if (availableObjects.Count == 0)
//             {
//                 return AddObject();
//             }
//
//             var obj = availableObjects.Dequeue();
//             obj.Activate();
//             obj.gameObject.SetActive(true);
//             return obj;
//         }
//
//         public void Recycle(T obj)
//         {
//             obj.Deactivate();
//             obj.Initialize();
//             obj.gameObject.SetActive(false);
//             if (availableObjects.Count < maxCapacity)
//             {
//                 availableObjects.Enqueue(obj);
//             }
//             else
//             {
//                 Object.Destroy(obj.gameObject);
//             }
//         }
//
//         public void Cleanup()
//         {
//             while (availableObjects.Count > 0)
//             {
//                 var obj = availableObjects.Dequeue();
//                 Object.Destroy(obj.gameObject);
//             }
//         }
//
//         private async Task CreateObjectsOverTimeAsync(int numberOfObjects, float delayBetweenInstances)
//         {
//             for (int i = 0; i < numberOfObjects; i++)
//             {
//                 var obj = Create();
//                 obj.gameObject.SetActive(false); // Assume initialization in an inactive state
//                 availableObjects.Enqueue(obj);
//                 await Task.Delay((int)(delayBetweenInstances * 1000));
//             }
//         }
//     }
// }
