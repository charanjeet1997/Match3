using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ModulerUISystem
{
	[ExecuteInEditMode]
    public class Traverse : Animatable
    {
        public Vector3 finalPosition;
        public List<Vector3> traversePoint;
        private int currentTargetIndex;
        private int lastTargetIndex;
        private float currentTime = 0;
        public override void OnAnimationStarted()
        {
            base.OnAnimationStarted();
            lastTargetIndex = 0;
            currentTargetIndex = 1;
        }
        public override void OnAnimationEnded()
        {
            base.OnAnimationEnded();
            lastTargetIndex = 0;
            currentTargetIndex = 1;
        }
        public override void OnAnimationRunning(float percentage)
        {
            base.OnAnimationRunning(percentage);
            if (currentTime <= duration)
            {
                currentTime += Time.fixedDeltaTime;
                rectTransform.anchoredPosition = Vector3.LerpUnclamped(traversePoint[lastTargetIndex],traversePoint[currentTargetIndex],curve.Evaluate(currentTime/duration));
            }
            else
            {
                currentTime = 0;
                currentTargetIndex++;
                lastTargetIndex++;
				if(currentTargetIndex==traversePoint.Count)
				{
					currentTargetIndex=0;
				}
				if(lastTargetIndex==traversePoint.Count)
				{
					lastTargetIndex=0;
				}
            }
        }
		[ContextMenu("Record")]
		public void Record()
		{
			traversePoint.Add(rectTransform.anchoredPosition);
		}
    }
}
