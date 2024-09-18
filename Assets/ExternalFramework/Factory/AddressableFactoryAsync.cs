// using System.Threading.Tasks;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.AddressableAssets;
// using UnityEngine.ResourceManagement.AsyncOperations;
//
// namespace Game.Factories
// {
//     public class AddressableFactoryAsync<T> : IFactory<T> where T : Component, IManagedLifecycle
//     {
//         private Queue<T> availableObjects = new Queue<T>();
//         private Queue<Task> loadingTasks = new Queue<Task>();
//         private AssetReference assetReference;
//         private int maxCapacity;
//         private bool startImmediately;
//         private int numberOfObjectsToCreate;
//         private float delayBetweenInstances;
//
//         public AddressableFactoryAsync(AssetReference assetReference, int maxCapacity = 50, 
//                                   bool startImmediately = true, int numberOfObjectsToCreate = 10, 
//                                   float delayBetweenInstances = 0.1f)
//         {
//             this.assetReference = assetReference;
//             this.maxCapacity = maxCapacity;
//             this.startImmediately = startImmediately;
//             this.numberOfObjectsToCreate = numberOfObjectsToCreate;
//             this.delayBetweenInstances = delayBetweenInstances;
//
//             if (startImmediately)
//             {
//                 Task.Run(() => PreloadObjectsAsync(numberOfObjectsToCreate));
//             }
//         }
//
//         private async Task PreloadObjectsAsync(int count)
//         {
//             for (int i = 0; i < count; i++)
//             {
//                 await LoadObjectAsync();
//                 await Task.Delay((int)(delayBetweenInstances * 1000));
//             }
//         }
//
//         public T Create()
//         {
//             if (availableObjects.Count > 0)
//             {
//                 var obj = availableObjects.Dequeue();
//                 obj.Activate();
//                 obj.gameObject.SetActive(true);
//                 return obj;
//             }
//             else if (loadingTasks.Count > 0 && loadingTasks.Peek().IsCompleted)
//             {
//                 loadingTasks.Dequeue(); // Remove completed loading task
//             }
//
//             if (loadingTasks.Count < maxCapacity)
//             {
//                 var loadTask = LoadObjectAsync();
//                 loadingTasks.Enqueue(loadTask);
//             }
//
//             return null; // Return null if object is not immediately available
//         }
//
//         private async Task LoadObjectAsync()
//         {
//             var handle = assetReference.InstantiateAsync();
//             await handle.Task;
//
//             if (handle.Status == AsyncOperationStatus.Succeeded)
//             {
//                 T obj = handle.Result.GetComponent<T>();
//                 obj.gameObject.SetActive(false);
//                 availableObjects.Enqueue(obj);
//             }
//             else
//             {
//                 Debug.LogError("Failed to load addressable object.");
//             }
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
//                 Addressables.ReleaseInstance(obj.gameObject);
//             }
//         }
//
//         public void Cleanup()
//         {
//             while (availableObjects.Count > 0)
//             {
//                 var obj = availableObjects.Dequeue();
//                 Addressables.ReleaseInstance(obj.gameObject);
//             }
//         }
//     }
// }
