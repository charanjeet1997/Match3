// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// namespace Game.Factories
// {
//     public class AssetBundleFactory<T> : IFactory<T> where T : Component, IManagedLifecycle
//     {
//         private Queue<T> availableObjects = new Queue<T>();
//         private string assetBundleUrl;
//         private string assetName;
//         private MonoBehaviour coroutineStarter;
//         private int maxCapacity;
//         private bool startImmediately;
//         private int numberOfObjectsToCreate;
//         private float delayBetweenInstances;
//
//         public AssetBundleFactory(MonoBehaviour coroutineStarter, string assetBundleUrl, string assetName,
//             int maxCapacity = 50, bool startImmediately = true,
//             int numberOfObjectsToCreate = 10, float delayBetweenInstances = 0.1f)
//         {
//             this.coroutineStarter = coroutineStarter;
//             this.assetBundleUrl = assetBundleUrl;
//             this.assetName = assetName;
//             this.maxCapacity = maxCapacity;
//             this.startImmediately = startImmediately;
//             this.numberOfObjectsToCreate = numberOfObjectsToCreate;
//             this.delayBetweenInstances = delayBetweenInstances;
//
//             if (startImmediately)
//             {
//                 StartCreation();
//             }
//         }
//
//         private void StartCreation()
//         {
//             coroutineStarter.StartCoroutine(CreateObjectsOverTime(numberOfObjectsToCreate, delayBetweenInstances));
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
//             coroutineStarter.StartCoroutine(LoadObjectFromAssetBundle());
//             return null;
//         }
//
//         private IEnumerator LoadObjectFromAssetBundle()
//         {
//             AssetBundleCreateRequest bundleLoadRequest = AssetBundle.LoadFromFileAsync(assetBundleUrl);
//             yield return bundleLoadRequest;
//
//             AssetBundle assetBundle = bundleLoadRequest.assetBundle;
//             if (assetBundle == null)
//             {
//                 Debug.LogError($"Failed to load AssetBundle from {assetBundleUrl}");
//                 yield break;
//             }
//
//             AssetBundleRequest assetLoadRequest = assetBundle.LoadAssetAsync<T>(assetName);
//             yield return assetLoadRequest;
//
//             T obj = assetLoadRequest.asset as T;
//             if (obj != null)
//             {
//                 T instance = Object.Instantiate(obj);
//                 instance.gameObject.SetActive(false);
//                 availableObjects.Enqueue(instance);
//             }
//             else
//             {
//                 Debug.LogError($"Failed to load asset {assetName} from {assetBundleUrl}");
//             }
//
//             assetBundle.Unload(false);
//         }
//
//         public void Recycle(T obj)
//         {
//             obj.Deactivate();
//             obj.Initialize();
//             obj.gameObject.SetActive(false);
//             availableObjects.Enqueue(obj);
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
//         public IEnumerator CreateObjectsOverTime(int numberOfObjects, float delayBetweenInstances)
//         {
//             for (int i = 0; i < numberOfObjects; i++)
//             {
//                 Create();
//                 yield return new WaitForSeconds(delayBetweenInstances);
//             }
//         }
//     }
// }