namespace ModulerUISystem
{
    
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class DoTransitionAfterSeconds : MonoBehaviour
    {
        [SerializeField] private ViewConfig targetViewConfig;
        [SerializeField] private float time;

        public void DoAction()
        {
            StartCoroutine(DoActionAfterSomeTime());
        }

        private IEnumerator DoActionAfterSomeTime()
        {
            
            yield return new WaitForSeconds(time);
            UIManager.instance.ShowView(targetViewConfig);
        }
    }

}