using System.Collections;
using System.Collections.Generic;
using Game.Factories;
using UnityEngine;

namespace Game.Factories
{
    public class RecycleAfterSometimeFactory<T> : IFactory<T> where T : Component
    {
        public string Name
        {
            get
            {
                return _factoryConfig.prefab.name;
            }
        }
        protected Queue<T> availableObjects = new Queue<T>();
        protected List<T> allObjects = new List<T>();
        protected Transform parentTransform;
        protected MonoBehaviour owner;
        protected FactoryConfig<T> _factoryConfig;
        protected float recycleTime;

        public RecycleAfterSometimeFactory(float recycleTime)
        {
            this.recycleTime = recycleTime;
        }
        
        public void Configure(FactoryConfig<T> config,MonoBehaviour factoryManager, Transform parent = null)
        {
            this.owner = factoryManager;
            this.parentTransform = parent;
            this._factoryConfig = config;
            if (config.startImmediately)
            {
                StartCreation();
            }
        }
        private void StartCreation()
        {
            owner.StartCoroutine(CreateObjectsOverTime(_factoryConfig.numberOfObjectsToCreate, _factoryConfig.delayBetweenInstances));
        }
        
        public virtual T AddObject()
        {
            var newObject = Object.Instantiate(_factoryConfig.prefab, parentTransform);
            newObject.gameObject.SetActive(false);
            availableObjects.Enqueue(newObject);
            allObjects.Add(newObject);
            return newObject;
        }
        
        public virtual T Create()
        {
            if (availableObjects.Count == 0)
            {
                AddObject();
            }

            var obj = availableObjects.Dequeue();
            if (obj is IManagedLifecycle<T> managedLifecycle)
            {
                managedLifecycle.Activate();
            }
            
            obj.gameObject.SetActive(true);
            CoroutineExtension.Execute(owner, recycleTime, () => Recycle(obj));
            return obj;
        }
        public void Recycle(T obj)
        {
            if(obj==null)
                return;
            if (obj is IManagedLifecycle<T> managedLifecycle)
            {
                managedLifecycle.Deactivate();
                managedLifecycle.Initialize();
            }
            obj.gameObject.SetActive(false);
            obj.transform.parent = parentTransform;
            availableObjects.Enqueue(obj);
        }

        public virtual void Cleanup()
        {
            foreach (var tempObject in allObjects)
            {
                if(tempObject!=null)
                    Object.Destroy(tempObject.gameObject);                
            }
            
            allObjects.Clear();
            availableObjects.Clear();
        }
        public IEnumerator CreateObjectsOverTime(int numberOfObjects, float delayBetweenInstances)
        {
            for (int i = 0; i < numberOfObjects; i++)
            {
                AddObject();
                yield return new WaitForSeconds(delayBetweenInstances);
            }
        }
    }
}