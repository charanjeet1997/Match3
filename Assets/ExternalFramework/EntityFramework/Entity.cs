namespace Games.UnnamedArcade3d.Entities.Transmoderna
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public class Entity : MonoBehaviour
    {
        #region PUBLIC_VARS
        #endregion

        #region PRIVATE_VARS
        #endregion

        #region UNITY_CALLBACKS
        public virtual void OnEnable()
        {
            EntityManager.instance.RegisterEntity(this);
            Debug.Log("Entiy registered : "+gameObject.name);
        }

        public virtual void OnDisable()
        {
            EntityManager.instance.UnRegisterEntity(this);
            Debug.Log("Entiy UnRegistered : " + gameObject.name);
        }

        #endregion

        #region PUBLIC_METHODS
        #endregion

        #region PRIVATE_METHODS       
        #endregion
    }
}
