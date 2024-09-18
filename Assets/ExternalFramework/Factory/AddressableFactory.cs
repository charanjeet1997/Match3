// using System.Collections;
// using System.Threading.Tasks;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.AddressableAssets;
// using UnityEngine.ResourceManagement.AsyncOperations;
//
// namespace Game.Factories
// {
//     public class AddressableFactory<T> : IFactory<T> where T : MonoBehaviour
//     {
//         private Queue<T> availableObjects = new Queue<T>();
//         private AssetReference assetReference;
//         private MonoBehaviour coroutineStarter;
//         private int maxCapacity;
//         private bool startImmediately;
//         private int numberOfObjectsToCreate;
//         private float delayBetweenInstances;
//         public string Name
//         {
//             get { return assetReference.Asset.name; }
//         }
//         public AddressableFactory(MonoBehaviour coroutineStarter, AssetReference assetReference, 
//             int maxCapacity = 50, bool startImmediately = true, 
//             int numberOfObjectsToCreate = 10, float delayBetweenInstances = 0.1f)
//         {
//             this.coroutineStarter = coroutineStarter;
//             this.assetReference = assetReference;
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
//         public void Configure(FactoryConfig<T> config)
//         {
//             
//         }
//
//
//         public T Create()
//         {
//             if (availableObjects.Count > 0)
//             {
//                 var obj = availableObjects.Dequeue();
//                
//                 obj.gameObject.SetActive(true);
//                 return obj;
//             }
//             // Load a new object if the pool is empty
//             coroutineStarter.StartCoroutine(LoadObject());
//             return null;
//         }
//
//         private IEnumerator LoadObject()
//         {
//             var handle = assetReference.InstantiateAsync();
//             yield return handle; // Wait for the handle to complete
//
//             if (handle.Status == AsyncOperationStatus.Succeeded)
//             {
//                 GameObject obj = handle.Result;
//                 T component = obj.GetComponent<T>();
//                 if (component != null)
//                 {
//                     component.gameObject.SetActive(false);  // Object is initially inactive
//                     availableObjects.Enqueue(component);    // Add to pool for future use
//                 }
//                 else
//                 {
//                     Debug.LogError("The instantiated object does not have the required component.");
//                     Addressables.ReleaseInstance(obj);
//                 }
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
//
//         
//
//         public IEnumerator CreateObjectsOverTime(int numberOfObjects, float delayBetweenInstances)
//         {
//             for (int i = 0; i < numberOfObjects; i++)
//             {
//                 Create();
//                 yield return new WaitForSeconds(delayBetweenInstances);
//             }
//         }
//
//         // Rest of the methods remain the same
//     }
// }
