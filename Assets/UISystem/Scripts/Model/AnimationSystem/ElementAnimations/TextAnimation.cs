using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
namespace ModulerUISystem
{
    public class TextAnimation : Animatable
    {
        public string[] texts;
        TextProvider textProvider;
        int i=0;
        private int stringIndex;
        public override void Awake()
        {
            base.Awake();
            textProvider=GetComponent<TextProvider>();
        }
        public override void OnAnimationStarted()
        {
            base.OnAnimationStarted();
        }
        public override void OnAnimationEnded()
        {
            base.OnAnimationEnded();
        }
        public override void OnAnimationRunning(float percentage)
        {
            base.OnAnimationRunning(percentage);
            textProvider.Text=texts[(i + 1) % texts.Length];
            i= (int)((texts.Length - 1)*percentage);
        }
        [ContextMenu("AddCounterTillHundered")]
        public void AddCounterTillHundered()
        {
            List<string> test = new List<string>();
            for (int index = 0; index <= 100; index++)
            {
                test.Add(index.ToString()+"%");
            }

            texts = test.ToArray();
        }
    }
}
