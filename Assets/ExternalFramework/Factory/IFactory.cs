using System.Collections;
using UnityEngine;

namespace Game.Factories
{
    public interface IFactory
    {
        string Name { get; }
        void Cleanup();
    }

    public interface IFactory<T> : IFactory
    {
        T Create();
        void Recycle(T obj);
        void Configure(FactoryConfig<T> config, MonoBehaviour factoryManager, Transform parent = null);
    }
}
