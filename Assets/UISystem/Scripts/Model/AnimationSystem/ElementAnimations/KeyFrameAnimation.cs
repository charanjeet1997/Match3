using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ModulerUISystem
{
    [System.Serializable]
    public class KeyFrame
    {
        public Vector3 Position;
        public Vector3 Scale;
        public float Rotation;
        public float time;
        public AnimationCurve curve;
    }
    [RequireComponent(typeof(RectTransform))]
    public class KeyFrameAnimation : Animatable
    {
        public List<KeyFrame> keyFrames;

        private int currentTargetIndex;
        private int lastTargetIndex;
        private float currentTime = 0;
        private float currentDuration;
        public override void Awake()
        {
            base.Awake();
        }
        public override void OnAnimationStarted()
        {
            base.OnAnimationStarted();
            lastTargetIndex = 0;
            currentTargetIndex = 1;
            currentDuration = keyFrames[lastTargetIndex].time;
            rectTransform.anchoredPosition = keyFrames[lastTargetIndex].Position;
            rectTransform.localScale = keyFrames[lastTargetIndex].Scale;
            rectTransform.localRotation=Quaternion.Euler(Vector3.forward*keyFrames[lastTargetIndex].Rotation);
        }
        public override void OnAnimationRunning(float percentage)
        {
            base.OnAnimationRunning(percentage);
            if (currentTime <= currentDuration)
            {
                currentTime += Time.fixedDeltaTime;
                rectTransform.anchoredPosition = Vector3.LerpUnclamped(keyFrames[lastTargetIndex].Position, keyFrames[currentTargetIndex].Position, keyFrames[lastTargetIndex].curve.Evaluate(currentTime / currentDuration));
                rectTransform.localScale = Vector3.LerpUnclamped(keyFrames[lastTargetIndex].Scale, keyFrames[currentTargetIndex].Scale, keyFrames[lastTargetIndex].curve.Evaluate(currentTime / currentDuration));
                rectTransform.localRotation = Quaternion.Lerp(  Quaternion.Euler(Vector3.forward*keyFrames[lastTargetIndex].Rotation), Quaternion.Euler(Vector3.forward * (keyFrames[currentTargetIndex].Rotation)) , curve.Evaluate(currentTime / currentDuration));
            }
            else
            {
                currentTime = 0;
                currentTargetIndex++;
                lastTargetIndex++;

                if (currentTargetIndex == keyFrames.Count)
                {
                    currentTargetIndex = 0;
                }
                if (lastTargetIndex == keyFrames.Count)
                {
                    lastTargetIndex = 0;
                }
                currentDuration = keyFrames[lastTargetIndex].time;
            }

        }
        public override void OnAnimationEnded()
        {
            base.OnAnimationEnded();
            lastTargetIndex = 0;
            currentTargetIndex = 1;
            currentDuration = keyFrames[keyFrames.Count-1].time;
            rectTransform.anchoredPosition = keyFrames[keyFrames.Count-1].Position;
            rectTransform.localScale = keyFrames[keyFrames.Count-1].Scale;
            rectTransform.localRotation=Quaternion.Euler(Vector3.forward*keyFrames[keyFrames.Count-1].Rotation);
        }
        [ContextMenu("Record")]
        public void Record()
        {
            KeyFrame keyFrame = new KeyFrame();
            keyFrame.Rotation = rectTransform.rotation.eulerAngles.z;
            keyFrame.Position = rectTransform.anchoredPosition;
            keyFrame.Scale = rectTransform.localScale;
            keyFrames.Add(keyFrame);
        }
        public int index;
        [ContextMenu("RecordOnIndex")]
        public void RecordOnIndex()
        {
            KeyFrame tempKeyFrame = keyFrames[index];
            tempKeyFrame.Rotation = rectTransform.rotation.eulerAngles.z;
            tempKeyFrame.Position = rectTransform.anchoredPosition;
            tempKeyFrame.Scale = rectTransform.localScale;
            keyFrames[index] = tempKeyFrame;
        }
    }
}
