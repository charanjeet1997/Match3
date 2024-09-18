// using System.Collections;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using UnityEngine;
//
// namespace Game.Factories
// {
//     public class AssetBundleFactoryAsync<T> : IFactory<T> where T : Component, IManagedLifecycle
//     {
//         private Queue<T> availableObjects = new Queue<T>();
//         private string assetBundleUrl;
//         private string assetName;
//         private int maxCapacity;
//         private bool startImmediately;
//         private int numberOfObjectsToCreate;
//         private float delayBetweenInstances;
//
//         public AssetBundleFactoryAsync(string assetBundleUrl, string assetName, int maxCapacity = 50,
//             bool startImmediately = true, int numberOfObjectsToCreate = 10,
//             float delayBetweenInstances = 0.1f)
//         {
//             this.assetBundleUrl = assetBundleUrl;
//             this.assetName = assetName;
//             this.maxCapacity = maxCapacity;
//             this.startImmediately = startImmediately;
//             this.numberOfObjectsToCreate = numberOfObjectsToCreate;
//             this.delayBetweenInstances = delayBetweenInstances;
//
//             if (startImmediately)
//             {
//                 PreloadObjectsAsync(numberOfObjectsToCreate);
//             }
//         }
//
//         private async void PreloadObjectsAsync(int count)
//         {
//             for (int i = 0; i < count; i++)
//             {
//                 var obj = await LoadObjectFromAssetBundleAsync();
//                 if (obj != null)
//                 {
//                     obj.gameObject.SetActive(false);
//                     availableObjects.Enqueue(obj);
//                 }
//
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
//
//             // The method returns null if an object is not immediately available.
//             // The calling code should handle this scenario appropriately.
//             return null;
//         }
//
//         private async Task<T> LoadObjectFromAssetBundleAsync()
//         {
//             await WaitForCompletion(AssetBundle.LoadFromFileAsync(assetBundleUrl));
//
//             AssetBundle assetBundle = AssetBundle.LoadFromFile(assetBundleUrl);
//             if (assetBundle == null)
//             {
//                 Debug.LogError($"Failed to load AssetBundle from {assetBundleUrl}");
//                 return null;
//             }
//
//             AssetBundleRequest assetLoadRequest = assetBundle.LoadAssetAsync<T>(assetName);
//             await WaitForCompletion(assetLoadRequest);
//
//             T obj = assetLoadRequest.asset as T;
//             assetBundle.Unload(false);
//             return obj;
//         }
//
//         private async Task WaitForCompletion(AsyncOperation asyncOperation)
//         {
//             var tcs = new TaskCompletionSource<bool>();
//             asyncOperation.completed += _ => tcs.SetResult(true);
//             await tcs.Task;
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
//     }
// }