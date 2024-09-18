// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// namespace Game.Factories
// {
//     [System.Serializable]
//     public class FactoryWrapper<T> where T : Component
//     {
//         [SerializeField] private GameObject prefab;
//         private IFactory<T> factory;
//
//         public IFactory<T> Factory
//         {
//             get
//             {
//                 if (factory == null)
//                 {
//                     // Assuming Factory<T> constructor takes a prefab of type T
//                     factory = new SimpleFactory<T>(prefab.GetComponent<T>());
//                 }
//
//                 return factory;
//             }
//         }
//     }
// }