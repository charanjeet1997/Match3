namespace Games.UnnamedArcade3d.Entities.Transmoderna
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class EntityManager : MonoBehaviour
    {
        #region PUBLIC_VARS
        public static EntityManager instance;
        public List<Entity> entities;
        
        #endregion

        #region PRIVATE_VARS
        #endregion

        #region UNITY_CALLBACKS
        void Awake()
        {
            instance=this;
        }
        #endregion

        #region PUBLIC_METHODS
        public void RegisterEntity(Entity entity)
        {
            if(!entities.Contains(entity))
            {
                entities.Add(entity);
                Debug.Log("Entity added : "+entity.gameObject.name);
               
            }
        }
        public void UnRegisterEntity(Entity entity)
        {
            if(entities.Contains(entity))
            {
                //entities.Remove(entity);
                Debug.Log("Entity removed : " + entity.gameObject.name);

            }
        }
        public void DestroyAllEntity()
        {
            Debug.Log("Entity count : "+entities.Count);
            if (!(entities.Count > 0))
                return;

            for(int indexOfEntity=entities.Count-1;indexOfEntity>=0;indexOfEntity--)
            {
                //entities.RemoveAt(indexOfEntity);
                Destroy(entities[indexOfEntity]);
            }
            entities.Clear();
        }
        #endregion

        #region PRIVATE_METHODS
        #endregion
    }
}