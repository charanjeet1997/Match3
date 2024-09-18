using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Factories
{
    [System.Serializable]
    public class FactoryConfig<T>
    {
        public T prefab;
        public string name;
        public bool startImmediately;
        public int numberOfObjectsToCreate;
        public float delayBetweenInstances;

        public FactoryConfig(T prefab, bool startImmediately = true, int numberOfObjectsToCreate = 10, float delayBetweenInstances = 0.1f)
        {
            this.prefab = prefab;
            this.startImmediately = startImmediately;
            this.numberOfObjectsToCreate = numberOfObjectsToCreate;
            this.delayBetweenInstances = delayBetweenInstances;
        }
    }
}